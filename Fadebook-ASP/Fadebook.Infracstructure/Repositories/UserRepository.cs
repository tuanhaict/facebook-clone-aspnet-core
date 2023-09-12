using AutoMapper;
using Fadebook.Application.Interfaces.Repositories;
using Fadebook.Application.Models.AuthModel;
using Fadebook.Domain.Entities;
using Fadebook.Infracstructure.Data;
using Fadebook.Infracstructure.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Infracstructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private ApplicationContext _context;
        private IMapper _mapper;
        public UserRepository(ApplicationContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            var appUser = await _context.Users.Include(c=> c.Introduction).FirstOrDefaultAsync(c => c.Email == email);
            var user = _mapper.Map<User>(appUser);
            return user;
        }

        public async Task<User> GetUserById(Guid id)
        {
            var appUser = await _context.Users.Include(c => c.Introduction).FirstOrDefaultAsync(c => c.Id == id);
            var user = _mapper.Map<User>(appUser);
            return user;
        }

        public async Task<IReadOnlyList<User>> GetUsersByFirstName(string firstName)
        {
            var appUsers = await _context.Users.Include(c=> c.Introduction).Where(c=> c.FirstName == firstName).ToListAsync();
            var users = _mapper.Map<IReadOnlyList<User>>(appUsers);
            return users;
        }

    }
}
