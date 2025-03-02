using AutoMapper;
using ObiletService.Core.Application.Dto;
using ObiletService.Core.Application.Features.Queries.Journeys.List;


namespace ObiletService.Core.Application.Mapping
{
    public class GeneralMappingProfile : Profile
    {
        public GeneralMappingProfile()
        {
            CreateMap<Journey, GetJourneysQueryResponse>();
            CreateMap<Journey, JourneyDto>().ForMember(dest => dest.Arrival, opt => opt.MapFrom(src => src.Arrival.ToString("HH:mm")))
                .ForMember(dest => dest.Departure, opt => opt.MapFrom(src => src.Departure.ToString("HH:mm")))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => $"{src.Price.ToString("0")} TL"));
        }
    }
}
