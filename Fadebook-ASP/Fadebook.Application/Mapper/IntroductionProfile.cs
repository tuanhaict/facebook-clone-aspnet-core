using AutoMapper;
using Fadebook.Application.Models.IntroductionModel;
using Fadebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Mapper
{
    public class IntroductionProfile : Profile
    {
        public IntroductionProfile()
        {
            CreateMap<IntroductionWriteDto, Introduction>().ReverseMap();
            CreateMap<IntroductionReadDto, Introduction>().ReverseMap();
        }
    }
}
