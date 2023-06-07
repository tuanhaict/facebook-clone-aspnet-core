using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Interfaces.Repositories
{
    public interface IRepositoryManager
    {
        public IUserRepository User { get; }
        public IPostRepository Post { get; }
        public ICommentRepository Comment { get; }
        public IReactionRepository Reaction { get; }
        public IIntroductionRepository Introduction { get; }
        public IFriendRepository Friend { get; }
        public Task SaveAsync();
    }
}
