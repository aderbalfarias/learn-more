using AutoMapper;
using LearnMore.Mvc.Dtos;
using LearnMore.Mvc.Models;

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