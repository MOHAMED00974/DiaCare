using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DiaCare.Domain.DTOS;
using DiaCare.Domain.Entities;

namespace DiaCare.Application.Profiles
{
    public class AuthProfile : Profile
    { 
        public AuthProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>().ReverseMap();
        }
    }
}
