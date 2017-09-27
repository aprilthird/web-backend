using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using FitGym.WS.Dtos;
using FitGym.WS.Models;

namespace FitGym.WS.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GymCompany, GymCompanyDto>();
            CreateMap<GymCompanyDto, GymCompany>().ForMember(m => m.GymCompanyId, opt => opt.Ignore());
        }
    }
}