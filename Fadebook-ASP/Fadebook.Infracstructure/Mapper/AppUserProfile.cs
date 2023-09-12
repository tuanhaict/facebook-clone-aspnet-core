using AutoMapper;
using Fadebook.Application.Models.UserModel;
using Fadebook.Domain.Entities;
using Fadebook.Infracstructure.AdapterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Infracstructure.Mapper
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<User, AppUser>().ReverseMap();
        }
        
    }
}
