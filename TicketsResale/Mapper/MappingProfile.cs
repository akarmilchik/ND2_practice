using AutoMapper;
using TicketsResale.Business.Models;
using TicketsResale.Controllers.Api.Models;

namespace TicketsResale.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventResource>()
               .ForMember(m => m.VenueName, opt => opt.MapFrom(src => src.Venue.Name))
               .ForMember(m => m.CityName, opt => opt.MapFrom(src => src.Venue.City.Name));

            CreateMap<Venue, VenueResource>();
        }
    }
}
