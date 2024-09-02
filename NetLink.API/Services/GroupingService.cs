using AutoMapper;
using NetLink.API.DTOs.Request;
using NetLink.API.Exceptions;
using NetLink.API.Models;
using NetLink.API.Repositories;

namespace NetLink.API.Services;

public interface IGroupingService
{
    Task<Guid> CreateGroupAsync(SensorGroupRequestDto groupDto, string endUserId);
    Task DeleteGroupAsync(Guid groupId, string endUserId);
    Task AddSensorToGroupAsync(Guid groupId, Guid sensorId, string endUserId);
    Task RemoveSensorFromGroupAsync(Guid groupId, Guid sensorId, string endUserId);
}

public class GroupingService(IEndUserService endUserService, IMapper mapper, ISensorGroupRepository sensorGroupRepository) : IGroupingService
{
    public async Task<Guid> CreateGroupAsync(SensorGroupRequestDto groupDto, string endUserId)
    {
        await endUserService.ValidateEndUserAsync(endUserId);

        if (await sensorGroupRepository.GroupNameExistsAsync(groupDto.GroupName!, endUserId))
        {
            throw new SensorGroupException("Group name already exists");
        }

        var group = mapper.Map<SensorGroup>(groupDto);

        var endUserSensorGroup = new EndUserSensorGroup
        {
            EndUserId = endUserId,
            SensorGroupId = group.Id
        };

        await sensorGroupRepository.AddGroupAsync(group);
        await sensorGroupRepository.AddEndUserSensorGroupAsync(endUserSensorGroup);
        await sensorGroupRepository.SaveChangesAsync();

        return group.Id;
    }

    public Task DeleteGroupAsync(Guid groupId, string endUserId)
    {
        throw new NotImplementedException();
    }

    public Task AddSensorToGroupAsync(Guid groupId, Guid sensorId, string endUserId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveSensorFromGroupAsync(Guid groupId, Guid sensorId, string endUserId)
    {
        throw new NotImplementedException();
    }
}