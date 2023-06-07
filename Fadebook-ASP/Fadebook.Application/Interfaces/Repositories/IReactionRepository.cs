using Fadebook.Application.Interfaces.Repositories.Base;
using Fadebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Interfaces.Repositories
{
    public interface IReactionRepository : IRepository<Reaction>
    {
        public Task<Reaction> CheckLikePost(Guid userId, Guid postId);
    }
}
