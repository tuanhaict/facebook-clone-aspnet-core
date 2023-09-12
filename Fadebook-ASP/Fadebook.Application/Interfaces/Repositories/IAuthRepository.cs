using Fadebook.Application.Models.AuthModel;
using Fadebook.Application.Models.TokenModel;
using Fadebook.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<Token> Singup(User user);
        Task<bool> CheckPassword(User user, string password);
        Token GetToken(Guid userId, IConfiguration _config);
        Token RefreshToken(string refreshToken);
    }
}
