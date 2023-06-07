using AutoMapper;
using Fadebook.Application.Interfaces;
using Fadebook.Application.Interfaces.Repositories;
using Fadebook.Application.Models.Constants;
using Fadebook.Application.Models.IntroductionModel;
using Fadebook.Application.Models.UserModel;
using Fadebook.Application.Services.Interfaces;
using Fadebook.Domain.Entities;
using Fadebook.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        public UserService(IRepositoryManager repositoryManager, IMapper mapper, IUploadService uploadService) 
        {
            _repository = repositoryManager;
            _mapper = mapper;
            _uploadService = uploadService;
        }
        public async Task<UserResponseDto> GetUserById(Guid id)
        {
            var user = await _repository.User.GetUserById(id);
            if (user == null)
            {
                throw new BadRequestException($"user with id: {id} not found!!");
            }
            var userResponseDto = _mapper.Map<UserResponseDto>(user);
            return userResponseDto;
        }

        public async Task<IReadOnlyList<UserResponseDto>> GetUsersByFirstName(string firstName)
        {
            var users = await _repository.User.GetUsersByFirstName(firstName);
            var usersResponseDto = _mapper.Map<IReadOnlyList<UserResponseDto>>(users);
            return usersResponseDto;
        }

        public async Task<UserResponseDto> GetUserByEmail(string email)
        {
            var user = await _repository.User.GetUserByEmail(email);
            var userResponseDto = _mapper.Map<UserResponseDto>(user);
            return userResponseDto;
        }
        public async Task UpdateUser(UserUpdateDto user, Guid userId)
        {
            var userEntity = await _repository.User.GetUserById(userId);
            if (user.FirstName != null) { userEntity.FirstName = user.FirstName; }
            if (user.LastName != null) { userEntity.LastName = user.LastName; }
            if (user.Email != null) { userEntity.Email = user.Email; }
            if (user.DateOfBirth != null) { userEntity.DateOfBirth = (DateTime)user.DateOfBirth; }
            await _repository.SaveAsync();

        }
        public async Task<string> UploadAvatar(IFormFile file, Guid userId)
        {
            var avatarUrl = await _uploadService.UploadImage(file, ImageTypes.Avatar);
            return avatarUrl;
        }
        public async Task<IntroductionReadDto> AddUserIntroduction(IntroductionWriteDto introduction, Guid userId)
        {
            var introductionEntity = _mapper.Map<Introduction>(introduction);
            introductionEntity.Id = userId;
            _repository.Introduction.Add(introductionEntity);
            await _repository.SaveAsync();
            var introductionReturn = _mapper.Map<IntroductionReadDto>(introductionEntity);
            return introductionReturn;

        }
        public async Task AddFriend(Guid userId, Guid friendId)
        {
            var isFriend = await _repository.Friend.CheckIsFriend(userId, friendId);
            if (isFriend != null)
            {
                throw new BadRequestException("Both are friends or you've already send friend request!");
            }
            else
            {
                var friend = new Friend
                {
                    Id = Guid.NewGuid(),
                    FirstId = userId,
                    SecondId = friendId,
                    Accepted = false
                };
                _repository.Friend.Add(friend);
                await _repository.SaveAsync();
            }
        }
        public async Task AcceptOrUnFriend(Guid userId, Guid friendId)
        {
            var isFriend = await _repository.Friend.CheckIsFriend(userId, friendId);
            if (isFriend == null)
            {
                throw new BadRequestException("This person haven't requested to be your friend!");
            }
            else
            {
                if (isFriend.Accepted == false && isFriend.SecondId == userId)
                {
                    isFriend.Accepted = true;
                    await _repository.SaveAsync();
                    return;
                }
                _repository.Friend.Delete(isFriend);
                await _repository.SaveAsync();

                
            }
        }

        public async Task<IReadOnlyList<UserForDisplayDto>> GetFriendsRequests(Guid userId)
        {
            var friendsRequests = await _repository.Friend.GetFriendsRequests(userId);  
            var friends = _mapper.Map<IReadOnlyList<UserForDisplayDto>>(friendsRequests);
            return friends;
        }

        public async Task<IReadOnlyList<UserForDisplayDto>> GetFriends(Guid userId)
        {
            var friends = await _repository.Friend.GetFriends(userId);
            var friendsReturn = _mapper.Map<IReadOnlyList<UserForDisplayDto>>(friends);
            return friendsReturn;
        }

    }
}
