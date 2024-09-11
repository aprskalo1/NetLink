using AutoMapper;
using NetLink.API.DTOs.Request;
using NetLink.API.DTOs.Response;
using NetLink.API.Exceptions;
using NetLink.API.Models;
using NetLink.API.Repositories;

namespace NetLink.API.Services;

public interface IGroupingService
{
    Task<Guid> CreateGroupAsync(GroupRequestDto groupDto, string endUserId);
    Task DeleteGroupAsync(Guid groupId, string endUserId);
    Task AddSensorToGroupAsync(Guid groupId, Guid sensorId, string endUserId);
    Task RemoveSensorFromGroupAsync(Guid groupId, Guid sensorId, string endUserId);
    Task<List<GroupResponseDto>> GetEndUserGroupsAsync(string endUserId);
    Task<GroupResponseDto> GetGroupByIdAsync(Guid groupId, string endUserId);
    Task<GroupResponseDto> UpdateGroupAsync(GroupRequestDto groupDto, Guid groupId, string endUserId);
}

public class GroupingService(IEndUserService endUserService, IMapper mapper, IGroupRepository groupRepository) : IGroupingService
{
    public async Task<Guid> CreateGroupAsync(GroupRequestDto groupDto, string endUserId)
    {
        await endUserService.ValidateEndUserAsync(endUserId);

        var group = mapper.Map<Group>(groupDto);
        await groupRepository.AddGroupAsync(group);

        var endUserGroup = new EndUserGroup
        {
            EndUserId = endUserId,
            GroupId = group.Id
        };

        await groupRepository.AddEndUserGroupAsync(endUserGroup);
        await groupRepository.SaveChangesAsync();

        return group.Id;
    }

    public async Task DeleteGroupAsync(Guid groupId, string endUserId)
    {
        await endUserService.ValidateEndUserAsync(endUserId);
        await groupRepository.ValidateUserGroupAsync(endUserId, groupId);

        var group = await groupRepository.GetGroupByIdAsync(groupId);

        if (group == null)
            throw new NotFoundException($"Group with ID: {groupId} has not been found.");

        await groupRepository.DeleteGroupAsync(group);
    }

    public async Task AddSensorToGroupAsync(Guid groupId, Guid sensorId, string endUserId)
    {
        await endUserService.ValidateEndUserAsync(endUserId);

        //TODO: Check if sensor belongs to the end user and if exists

        var group = await groupRepository.GetGroupByIdAsync(groupId);
        if (group == null)
            throw new NotFoundException($"Group with ID: {groupId} has not been found.");

        var existingSensorGroup = await groupRepository.GetSensorGroupAsync(groupId, sensorId);
        if (existingSensorGroup != null)
            throw new SensorGroupException($"Sensor with ID: {sensorId} already exists in group.");

        var sensorGroup = new SensorGroup
        {
            SensorId = sensorId,
            GroupId = groupId
        };

        await groupRepository.AddSensorGroupAsync(sensorGroup);
        await groupRepository.SaveChangesAsync();
    }

    public async Task RemoveSensorFromGroupAsync(Guid groupId, Guid sensorId, string endUserId)
    {
        await endUserService.ValidateEndUserAsync(endUserId);

        //TODO: Check if sensor belongs to the end user and if exists

        var sensorGroup = await groupRepository.GetSensorGroupAsync(groupId, sensorId);

        if (sensorGroup == null)
            throw new NotFoundException($"Sensor ({sensorId}) has not been found in group ({groupId}).");

        await groupRepository.RemoveSensorGroupAsync(sensorGroup);
    }

    public async Task<List<GroupResponseDto>> GetEndUserGroupsAsync(string endUserId)
    {
        await endUserService.ValidateEndUserAsync(endUserId);

        var groups = await groupRepository.FindEndUserGroupsAsync(endUserId);
        return mapper.Map<List<GroupResponseDto>>(groups);
    }

    public async Task<GroupResponseDto> GetGroupByIdAsync(Guid groupId, string endUserId)
    {
        await endUserService.ValidateEndUserAsync(endUserId);
        await groupRepository.ValidateUserGroupAsync(endUserId, groupId);

        var group = await groupRepository.GetGroupByIdAsync(groupId);
        if (group == null)
            throw new NotFoundException($"Group with ID: {groupId} has not been found.");

        return mapper.Map<GroupResponseDto>(group);
    }

    public async Task<GroupResponseDto> UpdateGroupAsync(GroupRequestDto groupDto, Guid groupId, string endUserId)
    {
        await endUserService.ValidateEndUserAsync(endUserId);
        await groupRepository.ValidateUserGroupAsync(endUserId, groupId);

        var existingGroup = await groupRepository.GetGroupByIdAsync(groupId);

        if (existingGroup == null)
            throw new NotFoundException($"Group with ID: {groupId} has not been found.");

        var group = mapper.Map(groupDto, existingGroup);
        await groupRepository.UpdateGroupAsync(group);

        return mapper.Map<GroupResponseDto>(group);
    }
}