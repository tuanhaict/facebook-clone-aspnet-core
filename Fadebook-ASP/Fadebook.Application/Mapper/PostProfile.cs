using AutoMapper;
using Fadebook.Application.Models.PostModel;
using Fadebook.Application.Models.UserModel;
using Fadebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Mapper
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostCreateDto, Post>().ReverseMap();
            CreateMap<PostUpdateDto, Post>().ReverseMap();
            CreateMap<Post, PostReturnDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => new UserForDisplayDto
                {
                    Id = src.User.Id,
                    FirstName = src.User.FirstName,
                    LastName = src.User.LastName,
                    Avatar = src.User.Avatar
                }))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Count))
                .ForMember(dest => dest.Reactions, opt => opt.MapFrom(src => src.Reactions.Count));
        }
    }
}
