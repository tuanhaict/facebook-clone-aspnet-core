using Fadebook.Application.Models.AuthModel;
using Fadebook.Application.Models.TokenModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        public Task<SignupResponseDto> Signup(SignupRequestDto signupRequestDto);
        public Token RefreshToken(string refreshToken);
    }
}
