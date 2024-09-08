using Microsoft.EntityFrameworkCore;
using NetLink.API.Data;
using NetLink.API.Exceptions;
using NetLink.API.Models;

namespace NetLink.API.Repositories;

public interface ISensorRepository
{
    Task AddSensorAsync(Sensor sensor);
    Task AddEndUserSensorAsync(EndUserSensor endUserSensor);
    Task<Sensor> GetSensorByIdAsync(Guid sensorId, string endUserId);
    Task<Sensor> GetSensorByNameAsync(string deviceName, string endUserId);
    Task<bool> DoesSensorExistAsync(string? sensorName, string endUserId);
    Task<Guid> GetSensorIdByNameAsync(string sensorName, string endUserId);
    Task DeleteSensorAsync(Sensor sensor);
    Task AddRecordedValueAsync(RecordedValue recordedValue);
    Task<List<RecordedValue>> GetRecordedValuesAsync(Guid sensorId, string endUserId, bool isAscending, int quantity);
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

    public async Task<Sensor> GetSensorByIdAsync(Guid sensorId, string endUserId)
    {
        var sensor = await dbContext.EndUserSensors
            .Where(e => e.EndUserId == endUserId && e.SensorId == sensorId)
            .Select(e => e.Sensor)
            .FirstOrDefaultAsync();

        return sensor ?? throw new NotFoundException($"Sensor with ID: {sensorId} has not been found.");
    }

    public async Task<Sensor> GetSensorByNameAsync(string deviceName, string endUserId)
    {
        var sensor = await dbContext.EndUserSensors
            .Where(e => e.EndUserId == endUserId && e.Sensor.DeviceName == deviceName)
            .Select(e => e.Sensor)
            .FirstOrDefaultAsync();

        return sensor ?? throw new NotFoundException($"Sensor with name: {deviceName} has not been found.");
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

    public async Task<List<RecordedValue>> GetRecordedValuesAsync(Guid sensorId, string endUserId, bool isAscending, int quantity)
    {
        var query = dbContext.RecordedValues
            .Where(r => r.SensorId == sensorId && r.Sensor.EndUserSensors.EndUserId == endUserId);

        var sortedQuery = isAscending ? query.OrderBy(rv => rv.RecordedAt) : query.OrderByDescending(rv => rv.RecordedAt);

        return await sortedQuery.Take(quantity).ToListAsync();
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