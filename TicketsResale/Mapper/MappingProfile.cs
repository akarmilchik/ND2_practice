using AutoMapper;
using TicketsResale.Business.Models;
using TicketsResale.Controllers.Api.Models;

namespace TicketsResale.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventResource>();
        }
    }
}
