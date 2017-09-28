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
            CreateMap<SubscriptionType, SubscriptionTypeDto>();
            CreateMap<SubscriptionTypeDto, SubscriptionType>().ForMember(m => m.SubscriptionTypeId, opt => opt.Ignore());
            CreateMap<Subscription, SubscriptionDto>();
            CreateMap<SubscriptionDto, Subscription>().ForMember(m => m.SubscriptionId, opt => opt.Ignore());
            CreateMap<PersonalTrainer, PersonalTrainerDto>();
            CreateMap<PersonalTrainerDto, PersonalTrainer>().ForMember(m => m.PersonalTrainerId, opt => opt.Ignore());
            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>().ForMember(m => m.ClientId, opt => opt.Ignore());
            CreateMap<Establishment, EstablishmentDto>();
            CreateMap<EstablishmentDto, Establishment>().ForMember(m => m.EstablishmentId, opt => opt.Ignore());
            CreateMap<ActivityType, ActivityTypeDto>();
            CreateMap<ActivityTypeDto, ActivityType>().ForMember(m => m.ActivityTypeId, opt => opt.Ignore());
            CreateMap<ActivityDetail, ActivityDetailDto>();
            CreateMap<ActivityDetailDto, ActivityDetail>().ForMember(m => m.ActivityDetailId, opt => opt.Ignore());
            CreateMap<Activity, ActivityDto>();
            CreateMap<ActivityDto, Activity>().ForMember(m => m.ActivityId, opt => opt.Ignore());
        }
    }
}