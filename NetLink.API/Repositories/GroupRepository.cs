using Microsoft.EntityFrameworkCore;
using NetLink.API.Data;
using NetLink.API.Exceptions;
using NetLink.API.Models;

namespace NetLink.API.Repositories;

public interface IGroupRepository
{
    Task AddGroupAsync(Group group);
    Task DeleteGroupAsync(Group group);
    Task AddEndUserGroupAsync(EndUserGroup endUserGroup);
    Task AddSensorGroupAsync(SensorGroup sensorGroup);
    Task RemoveSensorGroupAsync(SensorGroup sensorGroup);
    Task SaveChangesAsync();
    Task<Group> GetGroupByIdAsync(Guid groupId);
    Task<SensorGroup> GetSensorGroupAsync(Guid groupId, Guid sensorId);
    Task ValidateUserGroupAsync(string endUserId, Guid groupId);
    Task<List<Group>> FindEndUserGroupsAsync(string endUserId);
    Task UpdateGroupAsync(Group group);
}

public class GroupRepository(NetLinkDbContext dbContext) : IGroupRepository
{
    public async Task AddGroupAsync(Group group)
    {
        await dbContext.Groups.AddAsync(group);
    }

    public async Task DeleteGroupAsync(Group group)
    {
        dbContext.Groups.Remove(group);
        await SaveChangesAsync();
    }

    public async Task AddEndUserGroupAsync(EndUserGroup endUserGroup)
    {
        await dbContext.EndUserGroups.AddAsync(endUserGroup);
    }

    public async Task AddSensorGroupAsync(SensorGroup sensorGroup)
    {
        await dbContext.SensorGroups.AddAsync(sensorGroup);
    }

    public async Task RemoveSensorGroupAsync(SensorGroup sensorGroup)
    {
        dbContext.SensorGroups.Remove(sensorGroup);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task<Group> GetGroupByIdAsync(Guid groupId)
    {
        return (await dbContext.Groups.FindAsync(groupId))!;
    }

    public async Task<SensorGroup> GetSensorGroupAsync(Guid groupId, Guid sensorId)
    {
        return (await dbContext.SensorGroups.FirstOrDefaultAsync(x => x.GroupId == groupId && x.SensorId == sensorId))!;
    }

    public async Task ValidateUserGroupAsync(string endUserId, Guid groupId)
    {
        var endUserGroup = await dbContext.EndUserGroups.FirstOrDefaultAsync(x => x.EndUserId == endUserId && x.GroupId == groupId);

        if (endUserGroup == null)
            throw new SensorGroupException($"Group with ID: {groupId} not found.");
    }

    public async Task<List<Group>> FindEndUserGroupsAsync(string endUserId)
    {
        var groups = await dbContext.EndUserGroups
            .Where(eug => eug.EndUserId == endUserId)
            .Select(eug => eug.Group)
            .ToListAsync();

        return groups;
    }

    public Task UpdateGroupAsync(Group group)
    {
        dbContext.Groups.Update(group);
        return SaveChangesAsync();
    }
}