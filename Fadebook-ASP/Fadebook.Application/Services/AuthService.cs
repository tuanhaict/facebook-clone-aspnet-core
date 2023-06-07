using AutoMapper;
using Fadebook.Application.Extensions;
using Fadebook.Application.Interfaces.Repositories;
using Fadebook.Application.Models.AuthModel;
using Fadebook.Application.Models.Constants;
using Fadebook.Application.Models.TokenModel;
using Fadebook.Application.Services.Interfaces;
using Fadebook.Domain.Entities;
using Fadebook.Domain.Entities.Enums;
using Fadebook.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Fadebook.Application.Extensions.AuthExtension;

namespace Fadebook.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        public AuthService(IRepositoryManager repositoryManager, IMapper mapper, UserManager<User> userManager, IConfiguration configuration) 
        { 
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _userManager = userManager;
            _config = configuration;
        }

        public async Task<SignupResponseDto> Signup(SignupRequestDto signupRequestDto)
        {
            var user = _mapper.Map<User>(signupRequestDto);
            user.Id = Guid.NewGuid();
            user.UserName = user.Id.ToString();
            user.Avatar = user.Gender == Gender.Male ? Avatar.MALE_AVATAR : Avatar.FEMALE_AVATAR;
            await _userManager.CreateAsync(user, user.Password);
            
            var token = GenerateToken(user.Id, _config);
            var signupResponseDto = _mapper.Map<SignupResponseDto>(user);
            signupResponseDto.AccessToken = token.AccessToken;
            signupResponseDto.RefreshToken = token.RefreshToken;
            return signupResponseDto;
            
            

        }
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _repositoryManager.User.GetUserByEmail(loginRequestDto.Email);
            var result = (user != null && await _userManager.CheckPasswordAsync(user, loginRequestDto.Password));
            if (!result) throw new BadRequestException("Email or password is invalid");
            var loginResponseDto = _mapper.Map<LoginResponseDto>(user);
            var token = GenerateToken(user.Id, _config) ;
            loginResponseDto.AccessToken = token.AccessToken;
            loginResponseDto.RefreshToken =token.RefreshToken;
            return loginResponseDto;
        }
        public Token RefreshToken(string refreshToken)
        {
            var userId = AuthExtension.GetUserIdFromToken(refreshToken, _config);
            return GenerateToken(userId, _config) ;
        }
    }
}
