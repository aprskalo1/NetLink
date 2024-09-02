using Microsoft.EntityFrameworkCore;
using NetLink.API.Data;
using NetLink.API.Models;

namespace NetLink.API.Repositories;

public interface ISensorGroupRepository
{
    Task AddGroupAsync(SensorGroup group);
    Task<bool> GroupNameExistsAsync(string groupName, string endUserId);
    Task AddEndUserSensorGroupAsync(EndUserSensorGroup endUserSensorGroup);
    Task SaveChangesAsync();
}

public class SensorGroupRepository(NetLinkDbContext dbContext) : ISensorGroupRepository
{
    public async Task AddGroupAsync(SensorGroup group)
    {
        await dbContext.SensorGroups.AddAsync(group);
    }

    public async Task<bool> GroupNameExistsAsync(string groupName, string endUserId)
    {
        return await dbContext.SensorGroups.AnyAsync(g =>
            g.GroupName == groupName && g.EndUserSensorGroups!.Any(e => e.EndUserId == endUserId));
    }
    
    public async Task AddEndUserSensorGroupAsync(EndUserSensorGroup endUserSensorGroup)
    {
        await dbContext.EndUserSensorGroups.AddAsync(endUserSensorGroup);
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}