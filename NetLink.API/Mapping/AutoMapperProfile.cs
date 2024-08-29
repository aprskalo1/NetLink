using AutoMapper;
using NetLink.API.Models;
using NetLink.API.Shared;
using NetLink.API.Shared.DTOs;

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

        CreateMap<SensorRes, Sensor>();
        CreateMap<Sensor, SensorRes>();

        CreateMap<DeveloperRes, Developer>();
        CreateMap<Developer, DeveloperRes>();

        CreateMap<EndUserRes, EndUser>();
        CreateMap<EndUser, EndUserRes>();
    }
}