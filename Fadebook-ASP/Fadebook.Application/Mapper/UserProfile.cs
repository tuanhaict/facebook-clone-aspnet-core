using AutoMapper;
using Fadebook.Application.Models.AuthModel;
using Fadebook.Application.Models.UserModel;
using Fadebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserResponseDto>().ReverseMap();
            CreateMap<User, LoginRequestDto>().ReverseMap();
            CreateMap<User, SignupRequestDto>().ReverseMap();
            CreateMap<User, SignupResponseDto>().ReverseMap();
            CreateMap<User, LoginResponseDto>().ReverseMap();
            CreateMap<User, UserForDisplayDto>().ReverseMap();
        }
    }
}
