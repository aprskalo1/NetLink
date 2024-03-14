using AutoMapper;
using NetLink.API.DTOs;
using NetLink.API.Models;

namespace NetLink.API.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Sensor, SensorDTO>();
            CreateMap<SensorDTO, Sensor>();

            CreateMap<Developer, DeveloperDTO>();
            CreateMap<DeveloperDTO, Developer>();

            CreateMap<EndUser, EndUserDTO>();
            CreateMap<EndUserDTO, EndUser>();
        }
    }
}
