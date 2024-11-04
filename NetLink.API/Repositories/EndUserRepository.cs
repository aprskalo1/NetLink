using Microsoft.EntityFrameworkCore;
using NetLink.API.Data;
using NetLink.API.Exceptions;
using NetLink.API.Models;

namespace NetLink.API.Repositories;

public interface IEndUserRepository
{
    Task AddEndUserAsync(EndUser endUser);
    Task<EndUser> GetEndUserByIdAsync(string endUserId);
    Task<List<EndUser>> ListDeveloperEndUsersAsync(Guid developerId);

    Task<(List<EndUser> EndUsers, int TotalCount)> ListPagedDeveloperEndUsersAsync(Guid developerId, int page,
        int pageSize, string? searchTerm = null);

    Task AddDeveloperUserAsync(DeveloperUser developerUser);
    Task<List<Sensor>> GetSensorsByIdsAsync(List<Guid> sensorIds);
    Task<bool> IsEndUserSensorAssignedAsync(Guid sensorId, string endUserId);
    Task AddEndUserSensorsAsync(List<EndUserSensor> endUserSensors);
    Task SaveChangesAsync();
    Task<bool> ValidateEndUserAssociationAsync(string endUserId, Guid developerId);
    Task<List<Sensor>> ListEndUserSensorsAsync(string endUserId);

    Task<(List<Sensor> Sensors, int TotalCount)> ListPageEndUserSensorsAsync(string endUserId, int page, int pageSize,
        string? searchTerm = null);
}

public class EndUserRepository(NetLinkDbContext dbContext) : IEndUserRepository
{
    public async Task AddEndUserAsync(EndUser endUser)
    {
        await dbContext.EndUsers.AddAsync(endUser);
    }

    public async Task<EndUser> GetEndUserByIdAsync(string endUserId)
    {
        var endUser = await dbContext.EndUsers.FirstOrDefaultAsync(e => e.Id == endUserId);
        return endUser ?? throw new NotFoundException($"EndUser with Id {endUserId} not found.");
    }

    public async Task<List<EndUser>> ListDeveloperEndUsersAsync(Guid developerId)
    {
        return await dbContext.DeveloperUsers
            .Where(du => du.DeveloperId == developerId)
            .Select(du => du.EndUser)
            .ToListAsync();
    }

    public async Task<(List<EndUser> EndUsers, int TotalCount)> ListPagedDeveloperEndUsersAsync(
        Guid developerId,
        int page,
        int pageSize,
        string? searchTerm = null)
    {
        var query = dbContext.DeveloperUsers
            .Where(du => du.DeveloperId == developerId)
            .Select(du => du.EndUser);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(eu =>
                (eu.Id != null && eu.Id.ToString().Contains(searchTerm)) ||
                (eu.Username != null && eu.Username.Contains(searchTerm)) ||
                (eu.Email != null && eu.Email.Contains(searchTerm)) ||
                (eu.FirstName != null && eu.FirstName.Contains(searchTerm)) ||
                (eu.LastName != null && eu.LastName.Contains(searchTerm)));
        }

        var totalCount = await query.CountAsync();

        var endUsers = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (endUsers, totalCount);
    }

    public async Task AddDeveloperUserAsync(DeveloperUser developerUser)
    {
        await dbContext.DeveloperUsers.AddAsync(developerUser);
    }

    public async Task<List<Sensor>> GetSensorsByIdsAsync(List<Guid> sensorIds)
    {
        return await dbContext.Sensors.Where(s => sensorIds.Contains(s.Id)).ToListAsync();
    }

    public async Task<bool> IsEndUserSensorAssignedAsync(Guid sensorId, string endUserId)
    {
        return await dbContext.EndUserSensors.AnyAsync(eus => eus.SensorId == sensorId && eus.EndUserId == endUserId);
    }

    public async Task AddEndUserSensorsAsync(List<EndUserSensor> endUserSensors)
    {
        await dbContext.EndUserSensors.AddRangeAsync(endUserSensors);
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> ValidateEndUserAssociationAsync(string endUserId, Guid developerId)
    {
        return await dbContext.DeveloperUsers
            .AnyAsync(du => du.EndUserId == endUserId && du.DeveloperId == developerId);
    }

    public Task<List<Sensor>> ListEndUserSensorsAsync(string endUserId)
    {
        return dbContext.EndUserSensors
            .Where(eus => eus.EndUserId == endUserId)
            .Select(eus => eus.Sensor)
            .ToListAsync();
    }

    public async Task<(List<Sensor> Sensors, int TotalCount)> ListPageEndUserSensorsAsync(
        string endUserId,
        int page,
        int pageSize,
        string? searchTerm = null)
    {
        var query = dbContext.Sensors
            .Where(s => dbContext.EndUserSensors
                .Any(eus => eus.SensorId == s.Id && eus.EndUserId == endUserId));

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(s =>
                s.Id.ToString().Contains(searchTerm) ||
                (s.DeviceName != null && s.DeviceName.Contains(searchTerm)) ||
                (s.DeviceType != null && s.DeviceType.Contains(searchTerm)) ||
                (s.MeasurementUnit != null && s.MeasurementUnit.Contains(searchTerm)) ||
                (s.DeviceLocation != null && s.DeviceLocation.Contains(searchTerm)) ||
                (s.DeviceDescription != null && s.DeviceDescription.Contains(searchTerm)));
        }

        var totalCount = await query.CountAsync();

        var sensors = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (sensors, totalCount);
    }
}