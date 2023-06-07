using AutoMapper;
using Fadebook.Application.Interfaces;
using Fadebook.Application.Interfaces.Repositories;
using Fadebook.Application.Services.Interfaces;
using Fadebook.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IRepositoryManager _repository;
        private readonly Lazy<IAuthService> _authService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IPostService> _postService;
        private readonly Lazy<ICommentService> _commentService;
        public ServiceManager(IRepositoryManager repository, IMapper mapper, UserManager<User> userManager, IConfiguration configuration, IUploadService cloudinary)
        {
            _repository = repository;
            _authService = new Lazy<IAuthService>(() => new AuthService(repository, mapper, userManager,configuration ));
            _userService = new Lazy<IUserService>(()=> new UserService(repository, mapper, cloudinary));
            _postService = new Lazy<IPostService>(() => new PostService(repository, mapper, cloudinary));
            _commentService = new Lazy<ICommentService>(() => new CommentService(repository, mapper)); 
        }
        public IAuthService AuthService => _authService.Value;
        public IUserService UserService => _userService.Value;
        public ICommentService CommentService => _commentService.Value;
        public IPostService PostService => _postService.Value;

    }
}
