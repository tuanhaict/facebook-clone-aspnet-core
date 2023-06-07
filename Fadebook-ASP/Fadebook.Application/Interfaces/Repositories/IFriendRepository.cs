using Fadebook.Application.Interfaces.Repositories.Base;
using Fadebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Interfaces.Repositories
{
    public interface IFriendRepository : IRepository<Friend>
    {
        public Task<Friend> CheckIsFriend(Guid userId, Guid friendId);
        public Task<IReadOnlyList<User>> GetFriends(Guid userId);
        public Task<IReadOnlyList<User>> GetFriendsRequests(Guid userId);
    }
}
