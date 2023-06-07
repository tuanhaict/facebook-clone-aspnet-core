using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Services.Interfaces
{
    public interface IServiceManager
    {
        public IAuthService AuthService { get; }
        public IUserService UserService { get; }
        public IPostService PostService { get; }    
        public ICommentService CommentService { get; }
    }
}
