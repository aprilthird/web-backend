using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using FitGym.WS.Dtos;
using FitGym.WS.Models;
using Microsoft.Ajax.Utilities;

namespace FitGym.WS.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GymCompany, GymCompanyDto>().ForMember(m => m.Password, opt => opt.Ignore());
            CreateMap<GymCompanyDto, GymCompany>()
                .ForMember(m => m.GymCompanyId, opt => opt.Ignore())
                .ForMember(m => m.CreatedAt, opt => opt.Ignore())
                .ForMember(m => m.UpdatedAt, opt => opt.Ignore())
                .ForMember(m => m.Status, opt => opt.Ignore());
            CreateMap<SubscriptionType, SubscriptionTypeDto>();
            CreateMap<SubscriptionTypeDto, SubscriptionType>().ForMember(m => m.SubscriptionTypeId, opt => opt.Ignore());
            CreateMap<Subscription, SubscriptionDto>();
            CreateMap<SubscriptionDto, Subscription>().ForMember(m => m.SubscriptionId, opt => opt.Ignore());
            CreateMap<PersonalTrainer, PersonalTrainerDto>().ForMember(m => m.Password, opt => opt.Ignore());
            CreateMap<PersonalTrainerDto, PersonalTrainer>()
                .ForMember(m => m.PersonalTrainerId, opt => opt.Ignore())
                .ForMember(m => m.CreatedAt, opt => opt.Ignore())
                .ForMember(m => m.UpdatedAt, opt => opt.Ignore())
                .ForMember(m => m.Status, opt => opt.Ignore());
            CreateMap<Client, ClientDto>().ForMember(m => m.Password, opt => opt.Ignore());
            CreateMap<ClientDto, Client>()
                .ForMember(m => m.ClientId, opt => opt.Ignore())
                .ForMember(m => m.CreatedAt, opt => opt.Ignore())
                .ForMember(m => m.UpdatedAt, opt => opt.Ignore())
                .ForMember(m => m.Status, opt => opt.Ignore());
            CreateMap<Establishment, EstablishmentDto>();
            CreateMap<EstablishmentDto, Establishment>()
                .ForMember(m => m.EstablishmentId, opt => opt.Ignore())
                .ForMember(m => m.CreatedAt, opt => opt.Ignore())
                .ForMember(m => m.UpdatedAt, opt => opt.Ignore())
                .ForMember(m => m.Status, opt => opt.Ignore());
            CreateMap<ActivityType, ActivityTypeDto>();
            CreateMap<ActivityTypeDto, ActivityType>().ForMember(m => m.ActivityTypeId, opt => opt.Ignore());
            CreateMap<ActivityDetail, ActivityDetailDto>();
            CreateMap<ActivityDetailDto, ActivityDetail>().ForMember(m => m.ActivityDetailId, opt => opt.Ignore());
            CreateMap<Activity, ActivityDto>();
            CreateMap<ActivityDto, Activity>().ForMember(m => m.ActivityId, opt => opt.Ignore());
        }
    }
}