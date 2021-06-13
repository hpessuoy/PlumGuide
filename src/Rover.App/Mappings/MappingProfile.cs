using AutoMapper;
using Rover.App.Controllers.Rover.Dtos;
using Rover.Domain;

namespace Rover.App.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Direction, DirectionDto>();
            CreateMap<Location, LocationDto>()
                .ForMember(dest => dest.X, opt => opt.MapFrom(src => src.Coordinates.X))
                .ForMember(dest => dest.Y, opt => opt.MapFrom(src => src.Coordinates.Y))
                .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => src.Direction));
            
            CreateMap<MoveStatus, MoveStatusDto>();
            CreateMap<MoveResult, MoveResultDto>();

            CreateMap<Coordinates, CoordinatesDto>();
        }
    }
}
