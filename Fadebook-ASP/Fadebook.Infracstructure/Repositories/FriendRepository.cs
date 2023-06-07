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
    public class FriendRepository : RepositoryBase<Friend>, IFriendRepository
    {
        private readonly ApplicationContext _context;
        public FriendRepository(ApplicationContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Friend> CheckIsFriend(Guid userId, Guid friendId)
        {
            return await _context.Friends
                .FirstOrDefaultAsync(f => (f.FirstId == userId && f.SecondId == friendId) || (f.SecondId == userId && f.FirstId == friendId));
        }

        public async Task<IReadOnlyList<User>> GetFriends(Guid userId)
        {
            return await _context.Friends
                .Where(f => (f.FirstId == userId)  || (f.SecondId == userId))
                .Select( f => f.FirstId == userId ? f.SecondUser : f.FirstUser)
                .ToListAsync();

        }

        public async Task<IReadOnlyList<User>> GetFriendsRequests(Guid userId)
        {
            return await _context.Friends.Where(f => (f.SecondId == userId) && (f.Accepted == false))
                .Select(f => f.FirstUser)
                .ToListAsync();
        }

    }
}
