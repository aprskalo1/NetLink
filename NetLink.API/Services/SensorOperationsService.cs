using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetLink.API.Data;
using NetLink.API.Exceptions;
using NetLink.API.Models;
using NetLink.API.Shared;
using NetLink.API.Shared.DTOs;

namespace NetLink.API.Services;

public interface ISensorOperationsService
{
    Task<Guid> AddSensorAsync(SensorDto sensorDto, string endUserId);
    Task<SensorRes> GetSensorByNameAsync(string deviceName, string endUserId);
    Task<SensorRes> GetSensorByIdAsync(Guid sensorId, string endUserId);
    Task<SensorRes> UpdateSensorAsync(Guid sensorId, SensorDto sensorDto, string endUserId);
    Task DeleteSensorAsync(Guid sensorId, string endUserId);
    Task<Guid> AddRecordedValueAsync(RecordedValueDto recordedValueDto, string sensorName, string endUserId);
}

public class SensorOperationsService(IMapper mapper, NetLinkDbContext dbContext, IEndUserService endUserService)
    : ISensorOperationsService
{
    public async Task<Guid> AddSensorAsync(SensorDto sensorDto, string endUserId)
    {
        await endUserService.ValidateEndUserAsync(endUserId);

        if (await DoesSensorExistAsync(sensorDto.DeviceName!, endUserId))
        {
            throw new SensorException("Device with this name already exists, please choose another name.");
        }

        var sensor = mapper.Map<Sensor>(sensorDto);
        dbContext.Sensors.Add(sensor);

        var endUserSensor = new EndUserSensor
        {
            EndUserId = endUserId,
            SensorId = sensor.Id
        };
        dbContext.EndUserSensors.Add(endUserSensor);

        await dbContext.SaveChangesAsync();
        return sensor.Id;
    }

    public async Task<SensorRes> GetSensorByIdAsync(Guid sensorId, string endUserId)
    {
        var sensor = await FindSensorByIdAsync(sensorId, endUserId);
        return mapper.Map<SensorRes>(sensor);
    }

    public async Task<SensorRes> UpdateSensorAsync(Guid sensorId, SensorDto sensorDto, string endUserId)
    {
        var sensor = await FindSensorByIdAsync(sensorId, endUserId);

        mapper.Map(sensorDto, sensor);
        await dbContext.SaveChangesAsync();

        return mapper.Map<SensorRes>(sensor);
    }

    public async Task DeleteSensorAsync(Guid sensorId, string endUserId)
    {
        var sensor = await FindSensorByIdAsync(sensorId, endUserId);
        dbContext.Sensors.Remove(sensor);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Guid> AddRecordedValueAsync(RecordedValueDto recordedValueDto, string sensorName,
        string endUserId)
    {
        var sensorId = await GetSensorIdByNameAsync(sensorName, endUserId);

        var recordedValue = mapper.Map<RecordedValue>(recordedValueDto);
        recordedValue.SensorId = sensorId;

        dbContext.RecordedValues.Add(recordedValue);
        await dbContext.SaveChangesAsync();

        return recordedValue.Id;
    }

    public async Task<SensorRes> GetSensorByNameAsync(string deviceName, string endUserId)
    {
        await endUserService.ValidateEndUserAsync(endUserId);

        var sensor = await dbContext.EndUserSensors
            .Where(e => e.EndUserId == endUserId && e.Sensor!.DeviceName == deviceName)
            .Select(e => e.Sensor)
            .FirstOrDefaultAsync();

        if (sensor == null)
            throw new NotFoundException("Sensor not found.");

        return mapper.Map<SensorRes>(sensor);
    }

    private async Task<Sensor> FindSensorByIdAsync(Guid sensorId, string endUserId)
    {
        await endUserService.ValidateEndUserAsync(endUserId);

        var sensor = await dbContext.EndUserSensors
            .Where(e => e.EndUserId == endUserId && e.SensorId == sensorId)
            .Select(e => e.Sensor)
            .FirstOrDefaultAsync();

        if (sensor == null)
            throw new NotFoundException("Sensor not found.");

        return sensor;
    }

    private async Task<bool> DoesSensorExistAsync(string sensorName, string endUserId)
    {
        await endUserService.ValidateEndUserAsync(endUserId);

        var existingSensor = await dbContext.EndUserSensors
            .Include(e => e.Sensor)
            .FirstOrDefaultAsync(e => e.Sensor!.DeviceName == sensorName && e.EndUserId == endUserId);

        return existingSensor != null;
    }

    private async Task<Guid> GetSensorIdByNameAsync(string sensorName, string endUserId)
    {
        await endUserService.ValidateEndUserAsync(endUserId);

        var existingSensor = await dbContext.EndUserSensors
            .Include(e => e.Sensor)
            .FirstOrDefaultAsync(e => e.Sensor!.DeviceName == sensorName && e.EndUserId == endUserId);

        if (existingSensor == null)
        {
            throw new SensorException("Device with this name does not exist.");
        }

        return existingSensor.Sensor!.Id;
    }
}