using AutoMapper;
using Fadebook.Application.Interfaces.Repositories;
using Fadebook.Application.Models.AuthModel;
using Fadebook.Application.Models.Constants;
using Fadebook.Application.Models.TokenModel;
using Fadebook.Domain.Entities;
using Fadebook.Domain.Entities.Enums;
using Fadebook.Infracstructure.AdapterModel;
using Fadebook.Infracstructure.Data;
using Fadebook.Infracstructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Infracstructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private UserManager<AppUser> _userManager;
        private IConfiguration _configuration;
        private IMapper _mapper;
        private ApplicationContext _context;
        public AuthRepository(UserManager<AppUser> userManager,IConfiguration configuration, IMapper mapper, ApplicationContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _context = context;
        }
        public async Task<bool> CheckPassword(User user, string password)
        {
            var appUser = _mapper.Map<AppUser>(user);
            return await _userManager.CheckPasswordAsync(appUser, password);
        }


        public async Task<Token> Singup(User user)
        {
            var appUser = _mapper.Map<AppUser>(user);
            appUser.Id = Guid.NewGuid();
            Console.WriteLine("duma");
            appUser.UserName = appUser.Id.ToString();
            await _userManager.CreateAsync(appUser, appUser.Password);
            await _context.SaveChangesAsync();
            var token = GetToken(user.Id, _configuration);  
            return token;
        }

        public Token GetToken(Guid userId, IConfiguration _config)
        {
            return AuthExtension.GenerateToken(userId, _configuration);
        }

        public Token RefreshToken(string refreshToken)
        {
            var userId = AuthExtension.GetUserIdFromToken(refreshToken, _configuration);
            return GetToken(userId, _configuration);
        }
    }
}
