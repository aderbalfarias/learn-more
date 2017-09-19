using AutoMapper;
using LearnMore.Mvc.Core.Dtos;
using LearnMore.Mvc.Core.Models;

namespace LearnMore.Mvc
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Event, EventDto>();
            CreateMap<Notification, NotificationDto>();
        }
    }
}