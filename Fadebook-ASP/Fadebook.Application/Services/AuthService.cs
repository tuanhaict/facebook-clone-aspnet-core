using AutoMapper;
using Fadebook.Application.Interfaces.Repositories;
using Fadebook.Application.Models.AuthModel;
using Fadebook.Application.Models.Constants;
using Fadebook.Application.Models.TokenModel;
using Fadebook.Application.Services.Interfaces;
using Fadebook.Domain.Entities;
using Fadebook.Domain.Entities.Enums;
using Fadebook.Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public AuthService(IRepositoryManager repositoryManager, IMapper mapper, IConfiguration configuration) 
        { 
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _config = configuration;
        }

        public async Task<SignupResponseDto> Signup(SignupRequestDto signupRequestDto)
        {
            var user = _mapper.Map<User>(signupRequestDto);
            user.Avatar = user.Gender == Gender.Male ? Avatar.MALE_AVATAR : Avatar.FEMALE_AVATAR;
            var token = await _repositoryManager.Auth.Singup(user);
            var signupResponseDto = _mapper.Map<SignupResponseDto>(user);
            signupResponseDto.AccessToken = token.AccessToken;
            signupResponseDto.RefreshToken = token.RefreshToken;
            return signupResponseDto;
            
            

        }
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _repositoryManager.User.GetUserByEmail(loginRequestDto.Email);
            var result = (user != null && await _repositoryManager.Auth.CheckPassword(user, loginRequestDto.Password));
            if (!result) throw new BadRequestException("Email or password is invalid");
            var loginResponseDto = _mapper.Map<LoginResponseDto>(user);
            var token = _repositoryManager.Auth.GetToken(user.Id, _config);
            loginResponseDto.AccessToken = token.AccessToken;
            loginResponseDto.RefreshToken =token.RefreshToken;
            return loginResponseDto;
        }
        public Token RefreshToken(string refreshToken)
        {
            return _repositoryManager.Auth.RefreshToken(refreshToken);
        }
    }
}
