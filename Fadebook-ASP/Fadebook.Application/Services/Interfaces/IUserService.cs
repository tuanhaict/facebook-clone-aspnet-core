using Fadebook.Application.Models.IntroductionModel;
using Fadebook.Application.Models.UserModel;
using Fadebook.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Services.Interfaces
{
    public interface IUserService
    {
        public Task<IReadOnlyList<UserResponseDto>> GetUsersByFirstName(string firstName);
        public Task<UserResponseDto> GetUserByEmail(string email);
        public Task<UserResponseDto> GetUserById(Guid id);
        public Task UpdateUser(UserUpdateDto user, Guid userId);
        public Task<string> UploadAvatar(IFormFile file, Guid userId);
        public Task<IntroductionReadDto> AddUserIntroduction(IntroductionWriteDto introduction, Guid userId);
        public Task AddFriend(Guid userId, Guid friendId);
        public Task AcceptOrUnFriend(Guid userId, Guid friendId);
        public Task<IReadOnlyList<UserForDisplayDto>> GetFriendsRequests(Guid userId);
        public Task<IReadOnlyList<UserForDisplayDto>> GetFriends(Guid userId);
    }
}
