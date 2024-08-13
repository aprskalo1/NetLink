using AutoMapper;
using NetLink.API.DTOs;
using NetLink.API.Models;

namespace NetLink.API.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Sensor, SensorDto>();
        CreateMap<SensorDto, Sensor>();

        CreateMap<Developer, DeveloperDto>();
        CreateMap<DeveloperDto, Developer>();

        CreateMap<EndUser, EndUserDto>();
        CreateMap<EndUserDto, EndUser>();

        CreateMap<RecordedValue, RecordedValueDto>();
        CreateMap<RecordedValueDto, RecordedValue>();
    }
}