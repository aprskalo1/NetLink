using AutoMapper;
using NetLink.API.DTOs.Request;
using NetLink.API.DTOs.Response;
using NetLink.API.Exceptions;
using NetLink.API.Models;
using NetLink.API.Repositories;

namespace NetLink.API.Services;

public interface ISensorOperationsService
{
    Task<Guid> AddSensorAsync(SensorRequestDto sensorRequestDto, string endUserId);
    Task<SensorResponseDto> GetSensorByNameAsync(string deviceName, string endUserId);
    Task<SensorResponseDto> GetSensorByIdAsync(Guid sensorId, string endUserId);
    Task<SensorResponseDto> UpdateSensorAsync(Guid sensorId, SensorRequestDto sensorRequestDto, string endUserId);
    Task DeleteSensorAsync(Guid sensorId, string endUserId);
    Task RecordValueByNameAsync(RecordedValueRequestDto recordedValueRequestDto, string sensorName, string endUserId);
    Task RecordValueByIdAsync(RecordedValueRequestDto recordedValueRequestDto, Guid sensorId);
    Task RecordValueRemotelyAsync(RecordedValueRequestDto recordedValueRequestDto, Guid sensorId);
    Task<List<RecordedValueResponseDto>> GetRecordedValuesAsync(Guid sensorId, string endUserId, int quantity, bool isAscending = false);
}

public class SensorOperationsService(IMapper mapper, ISensorRepository sensorRepository, IEndUserService endUserService)
    : ISensorOperationsService
{
    public async Task<Guid> AddSensorAsync(SensorRequestDto sensorRequestDto, string endUserId)
    {
        await endUserService.ValidateEndUserAsync(endUserId);

        if (await sensorRepository.DoesSensorExistAsync(sensorRequestDto.DeviceName, endUserId))
        {
            throw new SensorException($"Device with name: {sensorRequestDto.DeviceName} already exists, please choose another name.");
        }

        var sensor = mapper.Map<Sensor>(sensorRequestDto);
        await sensorRepository.AddSensorAsync(sensor);

        var endUserSensor = new EndUserSensor
        {
            EndUserId = endUserId,
            SensorId = sensor.Id
        };
        await sensorRepository.AddEndUserSensorAsync(endUserSensor);
        await sensorRepository.SaveChangesAsync();

        return sensor.Id;
    }

    public async Task<SensorResponseDto> GetSensorByIdAsync(Guid sensorId, string endUserId)
    {
        var sensor = await sensorRepository.GetSensorByIdAsync(sensorId, endUserId);
        return mapper.Map<SensorResponseDto>(sensor);
    }

    public async Task<SensorResponseDto> UpdateSensorAsync(Guid sensorId, SensorRequestDto sensorRequestDto, string endUserId)
    {
        var sensor = await sensorRepository.GetSensorByIdAsync(sensorId, endUserId);

        mapper.Map(sensorRequestDto, sensor);
        await sensorRepository.SaveChangesAsync();

        return mapper.Map<SensorResponseDto>(sensor);
    }

    public async Task DeleteSensorAsync(Guid sensorId, string endUserId)
    {
        var sensor = await sensorRepository.GetSensorByIdAsync(sensorId, endUserId);
        await sensorRepository.DeleteSensorAsync(sensor);
    }

    public async Task<SensorResponseDto> GetSensorByNameAsync(string deviceName, string endUserId)
    {
        await endUserService.ValidateEndUserAsync(endUserId);

        var sensor = await sensorRepository.GetSensorByNameAsync(deviceName, endUserId);
        return mapper.Map<SensorResponseDto>(sensor);
    }

    public async Task RecordValueByNameAsync(RecordedValueRequestDto recordedValueRequestDto, string sensorName, string endUserId)
    {
        var sensorId = await sensorRepository.GetSensorIdByNameAsync(sensorName, endUserId);

        var recordedValue = mapper.Map<RecordedValue>(recordedValueRequestDto);
        recordedValue.SensorId = sensorId;

        await sensorRepository.AddRecordedValueAsync(recordedValue);
        await sensorRepository.SaveChangesAsync();
    }

    public async Task RecordValueByIdAsync(RecordedValueRequestDto recordedValueRequestDto, Guid sensorId)
    {
        var recordedValue = mapper.Map<RecordedValue>(recordedValueRequestDto);
        recordedValue.SensorId = sensorId;

        await sensorRepository.AddRecordedValueAsync(recordedValue);
        await sensorRepository.SaveChangesAsync();
    }

    public async Task RecordValueRemotelyAsync(RecordedValueRequestDto recordedValueRequestDto, Guid sensorId)
    {
        var recordedValue = mapper.Map<RecordedValue>(recordedValueRequestDto);
        recordedValue.SensorId = sensorId;

        await sensorRepository.AddRecordedValueAsync(recordedValue);
        await sensorRepository.SaveChangesAsync();
    }

    public async Task<List<RecordedValueResponseDto>> GetRecordedValuesAsync(Guid sensorId, string endUserId, int quantity, bool isAscending = false)
    {
        switch (quantity)
        {
            case < 1:
                throw new RecordedValueException("Quantity must be greater than 0.");
            case > 100:
                throw new RecordedValueException("Quantity must be less than or equal to 100.");
        }
        
        await endUserService.ValidateEndUserAsync(endUserId);
        
        var recordedValues = await sensorRepository.GetRecordedValuesAsync(sensorId, endUserId, isAscending, quantity);
        
        return mapper.Map<List<RecordedValueResponseDto>>(recordedValues);
    }
}