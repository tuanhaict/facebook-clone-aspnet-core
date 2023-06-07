using AutoMapper;
using Fadebook.Application.Extensions;
using Fadebook.Application.Interfaces;
using Fadebook.Application.Interfaces.Repositories;
using Fadebook.Application.Models.Constants;
using Fadebook.Application.Models.PostModel;
using Fadebook.Application.Services.Interfaces;
using Fadebook.Domain.Entities;
using Fadebook.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IUploadService _cloudinary;
        public PostService(IRepositoryManager repository, IMapper mapper, IUploadService cloudinary) 
        {
            _repository = repository;
            _mapper = mapper;
            _cloudinary = cloudinary;
        }

        public async Task<PostReturnDto> CreatePost(PostCreateDto post, Guid userId)
        {
            var imageUrl = await _cloudinary.UploadImage(post.Image, ImageTypes.PostImage);
            var postEntity = _mapper.Map<Post>(post);
            postEntity.Id = Guid.NewGuid();
            postEntity.UserId = userId;
            postEntity.Image = imageUrl;
            _repository.Post.Add(postEntity);
            await _repository.SaveAsync();
            var postReturn = _mapper.Map<PostReturnDto>(postEntity);
            return postReturn;

        }

        public async Task<PostReturnDto> GetPost(Guid id)
        {
            var postEntity = await _repository.Post.GetPost(id);
            if (postEntity == null)
            {
                throw new NotFoundException($"Post with id: {id} doesn't exists");
            }
            var postReturn = _mapper.Map<PostReturnDto>(postEntity); 
            return postReturn;
        }
        public async Task<IReadOnlyList<PostReturnDto>> GetPosts()
        {
            var postsEntities = await _repository.Post.GetPosts();
            var postsReturn = _mapper.Map<IReadOnlyList<PostReturnDto>>(postsEntities);
            return postsReturn;
        }
        public async Task UpdatePost(PostUpdateDto post, Guid id, Guid userId)
        {
            var postEntity = await ValidatePermissions<Post>.Validate(_repository.Post, id, userId);
            if (post.Caption != null) { postEntity.Caption = post.Caption.Trim(); }
            if (post.Image != null) 
            {
                var imageUrl = await _cloudinary.UploadImage(post.Image, ImageTypes.PostImage);
                postEntity.Image = imageUrl;
            }
            await _repository.SaveAsync();

        }
        public async Task DeletePost(Guid id, Guid userId)
        {
            var postEntity = await ValidatePermissions<Post>.Validate(_repository.Post, id, userId);
            _repository.Post.Delete(postEntity);
            await _repository.SaveAsync();  
        }
        public async Task ToogleLikePost(Guid userId, Guid postId)
        {
            var isLiked = await _repository.Reaction.CheckLikePost(userId, postId);
            if (isLiked != null)
            {
                _repository.Reaction.Delete(isLiked);
                await _repository.SaveAsync();
            }
            else
            {
                var liked = new Reaction
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    PostId = postId
                };
                _repository.Reaction.Add(liked);
                await _repository.SaveAsync();
            }
        }
    }
}
