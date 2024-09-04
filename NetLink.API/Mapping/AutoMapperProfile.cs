using AutoMapper;
using NetLink.API.DTOs.Request;
using NetLink.API.DTOs.Response;
using NetLink.API.Models;

namespace NetLink.API.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Sensor, SensorRequestDto>();
        CreateMap<SensorRequestDto, Sensor>();

        CreateMap<Developer, DeveloperRequestDto>();
        CreateMap<DeveloperRequestDto, Developer>();

        CreateMap<EndUser, EndUserRequestDto>();
        CreateMap<EndUserRequestDto, EndUser>();

        CreateMap<RecordedValue, RecordedValueRequestDto>();
        CreateMap<RecordedValueRequestDto, RecordedValue>();

        CreateMap<SensorResponseDto, Sensor>();
        CreateMap<Sensor, SensorResponseDto>();

        CreateMap<DeveloperResponseDto, Developer>();
        CreateMap<Developer, DeveloperResponseDto>();

        CreateMap<EndUserResponseDto, EndUser>();
        CreateMap<EndUser, EndUserResponseDto>();

        CreateMap<GroupRequestDto, Group>();
        CreateMap<Group, GroupRequestDto>();
        
        CreateMap<GroupResponseDto, Group>();
        CreateMap<Group, GroupResponseDto>();
    }
}