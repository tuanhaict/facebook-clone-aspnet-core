using AutoMapper;
using Fadebook.Application.Extensions;
using Fadebook.Application.Interfaces.Repositories;
using Fadebook.Application.Models.CommentModel;
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
    public class CommentService : ICommentService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public CommentService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<CommentReturnDto>> GetCommentsOfPost(Guid postId)
        {
            var comments = await _repository.Comment.GetCommentsOfPost(postId);
            var commentsReturn = _mapper.Map<IReadOnlyList<CommentReturnDto>>(comments);
            return commentsReturn;
        }
        public async Task UpdateComment(CommentWriteDto comment, Guid commentId, Guid userId)
        {
            var commentEntity = await ValidatePermissions<Comment>.Validate(_repository.Comment, commentId, userId);
            commentEntity.Content = comment.Content;
            await _repository.SaveAsync();
        }
        public async Task<CommentReturnDto> CreateComment(CommentWriteDto comment, Guid userId, Guid postId)
        {
            var commentEntity = _mapper.Map<Comment>(comment);
            commentEntity.UserId = userId;
            commentEntity.PostId = postId;
            commentEntity.Id = Guid.NewGuid();
            _repository.Comment.Add(commentEntity);
            await _repository.SaveAsync();
            var commentReturn = _mapper.Map<CommentReturnDto>(commentEntity);
            return commentReturn;
        }
        public async Task DeleteComment(Guid commentId, Guid userId)
        {
            var commentEntity = await ValidatePermissions<Comment>.Validate(_repository.Comment, commentId, userId);
            _repository.Comment.Delete(commentEntity);
            await _repository.SaveAsync();
        }
    }
}
