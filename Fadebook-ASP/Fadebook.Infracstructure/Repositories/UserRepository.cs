using Fadebook.Application.Interfaces.Repositories;
using Fadebook.Domain.Entities;
using Fadebook.Infracstructure.Data;
using Fadebook.Infracstructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Infracstructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private ApplicationContext _context;
        public UserRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.Include(c=> c.Introduction).FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _context.Users.Include(c => c.Introduction).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IReadOnlyList<User>> GetUsersByFirstName(string firstName)
        {
            return await _context.Users.Include(c=> c.Introduction).Where(c=> c.FirstName == firstName).ToListAsync();
        }
    }
}
