using Microsoft.EntityFrameworkCore;
using NetLink.API.Data;
using NetLink.API.Exceptions;
using NetLink.API.Models;

namespace NetLink.API.Repositories;

public interface ISensorRepository
{
    Task AddSensorAsync(Sensor sensor);
    Task AddEndUserSensorAsync(EndUserSensor endUserSensor);
    Task<Sensor> GetEndUserSensorByIdAsync(Guid sensorId, string endUserId);
    Task<Sensor> GetEndUserSensorByNameAsync(string deviceName, string endUserId);
    Task<Sensor?> GetSensorByIdAsync(Guid sensorId);
    Task<bool> DoesSensorExistAsync(string? sensorName, string endUserId);
    Task<Guid> GetSensorIdByNameAsync(string sensorName, string endUserId);
    Task DeleteSensorAsync(Sensor sensor);
    Task AddRecordedValueAsync(RecordedValue recordedValue);
    Task<List<Sensor>> GetSensorFromGroupAsync(Guid groupId);

    Task<List<RecordedValue>> GetRecordedValuesAsync(Guid sensorId, string endUserId, bool isAscending,
        int? quantity = null, DateTime? startDate = null,
        DateTime? endDate = null);

    Task SaveChangesAsync();
}

public class SensorRepository(NetLinkDbContext dbContext) : ISensorRepository
{
    public async Task AddSensorAsync(Sensor sensor)
    {
        await dbContext.Sensors.AddAsync(sensor);
    }

    public async Task AddEndUserSensorAsync(EndUserSensor endUserSensor)
    {
        await dbContext.EndUserSensors.AddAsync(endUserSensor);
    }

    public async Task<Sensor> GetEndUserSensorByIdAsync(Guid sensorId, string endUserId)
    {
        var sensor = await dbContext.EndUserSensors
            .Where(e => e.EndUserId == endUserId && e.SensorId == sensorId)
            .Select(e => e.Sensor)
            .FirstOrDefaultAsync();

        return sensor ?? throw new NotFoundException($"Sensor with ID: {sensorId} has not been found.");
    }

    public async Task<Sensor> GetEndUserSensorByNameAsync(string deviceName, string endUserId)
    {
        var sensor = await dbContext.EndUserSensors
            .Where(e => e.EndUserId == endUserId && e.Sensor.DeviceName == deviceName)
            .Select(e => e.Sensor)
            .FirstOrDefaultAsync();

        return sensor ?? throw new NotFoundException($"Sensor with name: {deviceName} has not been found.");
    }

    public async Task<Sensor?> GetSensorByIdAsync(Guid sensorId)
    {
        var sensor = await dbContext.Sensors.FirstOrDefaultAsync(s => s.Id == sensorId);
        return sensor;
    }

    public async Task<bool> DoesSensorExistAsync(string? sensorName, string endUserId)
    {
        var existingSensor = await dbContext.EndUserSensors
            .Include(e => e.Sensor)
            .FirstOrDefaultAsync(e => e.Sensor.DeviceName == sensorName && e.EndUserId == endUserId);

        return existingSensor != null;
    }

    public async Task<Guid> GetSensorIdByNameAsync(string sensorName, string endUserId)
    {
        var existingSensor = await dbContext.EndUserSensors
            .Include(e => e.Sensor)
            .FirstOrDefaultAsync(e => e.Sensor.DeviceName == sensorName && e.EndUserId == endUserId);

        if (existingSensor == null)
        {
            throw new SensorException($"Device with name: {sensorName} does not exist.");
        }

        return existingSensor.Sensor.Id;
    }

    public async Task AddRecordedValueAsync(RecordedValue recordedValue)
    {
        await dbContext.RecordedValues.AddAsync(recordedValue);
    }

    public async Task<List<Sensor>> GetSensorFromGroupAsync(Guid groupId)
    {
        var sensors = await dbContext.SensorGroups
            .Where(sg => sg.GroupId == groupId)
            .Select(sg => sg.Sensor)
            .ToListAsync();

        return sensors;
    }

    public async Task<List<RecordedValue>> GetRecordedValuesAsync(
        Guid sensorId,
        string endUserId,
        bool isAscending,
        int? quantity = null,
        DateTime? startDate = null,
        DateTime? endDate = null)
    {
        var query = dbContext.RecordedValues
            .Where(r => r.SensorId == sensorId && r.Sensor.EndUserSensors.EndUserId == endUserId);

        if (startDate.HasValue && endDate.HasValue)
        {
            query = query.Where(rv => rv.RecordedAt >= startDate.Value && rv.RecordedAt <= endDate.Value);
        }

        query = isAscending ? query.OrderBy(rv => rv.RecordedAt) : query.OrderByDescending(rv => rv.RecordedAt);

        if (!startDate.HasValue && !endDate.HasValue)
        {
            query = query.Take(quantity ?? 1);
        }

        return await query.ToListAsync();
    }


    public async Task DeleteSensorAsync(Sensor sensor)
    {
        dbContext.Sensors.Remove(sensor);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}