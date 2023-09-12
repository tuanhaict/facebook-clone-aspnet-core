using Fadebook.Application.Interfaces.Repositories.Base;
using Fadebook.Application.Models.AuthModel;
using Fadebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(Guid id);
        Task<IReadOnlyList<User>> GetUsersByFirstName(string firstName);
    }
}
