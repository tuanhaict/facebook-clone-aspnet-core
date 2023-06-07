using Fadebook.Application.Interfaces.Repositories;
using Fadebook.Infracstructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Infracstructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationContext _context;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IPostRepository> _postRepository;
        private readonly Lazy<ICommentRepository> _commentRepository;
        private readonly Lazy<IReactionRepository> _reactionRepository;
        private readonly Lazy<IIntroductionRepository> _introductionRepository;
        private readonly Lazy<IFriendRepository> _friendRepository;
        public RepositoryManager(ApplicationContext applicationContext)
        {
            _context = applicationContext;
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
            _postRepository = new Lazy<IPostRepository>(()=> new PostRepository(_context));
            _commentRepository = new Lazy<ICommentRepository>(()=> new CommentRepository(_context));
            _reactionRepository = new Lazy<IReactionRepository>(()=> new ReactionRepository(_context));
            _introductionRepository = new Lazy<IIntroductionRepository>(()=> new IntroductionRepository(_context));
            _friendRepository = new Lazy<IFriendRepository>(() => new FriendRepository(_context));
        }
        public IUserRepository User=> _userRepository.Value;
        public IPostRepository Post => _postRepository.Value;
        public ICommentRepository Comment=> _commentRepository.Value;
        public IReactionRepository Reaction => _reactionRepository.Value;
        public IIntroductionRepository Introduction => _introductionRepository.Value;
        public IFriendRepository Friend => _friendRepository.Value;
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
