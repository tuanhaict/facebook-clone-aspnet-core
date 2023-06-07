using AutoMapper;
using Fadebook.Application.Models.CommentModel;
using Fadebook.Application.Models.UserModel;
using Fadebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Mapper
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentReturnDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => new UserForDisplayDto
                {
                    Id = src.User.Id,
                    FirstName = src.User.FirstName,
                    LastName = src.User.LastName,
                    Avatar = src.User.Avatar,
                }));
            CreateMap<Comment, CommentWriteDto>().ReverseMap();
        }
    }
}
