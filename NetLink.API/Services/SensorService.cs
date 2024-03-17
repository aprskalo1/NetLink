using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetLink.API.Data;
using NetLink.API.DTOs;
using NetLink.API.Exceptions;
using NetLink.API.Models;

namespace NetLink.API.Services
{

    public interface ISensorService
    {
        Task<SensorDTO> AddSensorAsync(SensorDTO sensorDTO, string endUserId);
        Task<SensorDTO> GetSensorByIdAsync(Guid id, string endUserId);
        Task<RecordedValueDTO> AddRecordedValueAsync(RecordedValueDTO recordedValueDTO, string sensorName, string endUserId);
    }

    public class SensorService : ISensorService
    {
        private readonly IMapper _mapper;
        private readonly NetLinkDbContext _dbContext;
        private readonly IEndUserService _endUserService;

        public SensorService(IMapper mapper, NetLinkDbContext dbContext, IEndUserService endUserService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _endUserService = endUserService;
        }

        public async Task<SensorDTO> AddSensorAsync(SensorDTO sensorDTO, string endUserId)
        {
            await _endUserService.CheckIfEndUserExistsAsync(endUserId);
            await CheckIfSensorForUserDoesntExistAsync(sensorDTO.DeviceName!, endUserId);

            var sensor = _mapper.Map<Sensor>(sensorDTO);
            _dbContext.Sensors.Add(sensor);

            var endUserSensor = new EndUserSensor
            {
                EndUserId = endUserId,
                SensorId = sensor.Id
            };
            _dbContext.EndUserSensors.Add(endUserSensor);

            await _dbContext.SaveChangesAsync();
            return sensorDTO;
        }

        public async Task<RecordedValueDTO> AddRecordedValueAsync(RecordedValueDTO recordedValueDTO, string sensorName, string endUserId)
        {
            var sensorId = await FindSensorByNameAsync(sensorName, endUserId);

            var recordedValue = _mapper.Map<RecordedValue>(recordedValueDTO);
            recordedValue.SensorId = sensorId;

            _dbContext.RecordedValues.Add(recordedValue);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<RecordedValueDTO>(recordedValue);
        }

        public async Task<SensorDTO> GetSensorByIdAsync(Guid id, string endUserId)
        {
            var endUser = await _endUserService.GetUserByIdAsync(endUserId);

            var sensor = await _dbContext.EndUserSensors
                .Include(e => e.Sensor)
                .Where(e => e.EndUserId == endUserId)
                .Select(e => e.Sensor)
                .FirstOrDefaultAsync(s => s!.Id == id);

            if (sensor == null)
                throw new NotFoundException("Sensor not found.");

            return _mapper.Map<SensorDTO>(sensor);
        }

        private async Task CheckIfSensorForUserDoesntExistAsync(string sensorName, string endUserId)
        {
            var existingSensor = await _dbContext.EndUserSensors
                .Include(e => e.Sensor)
                .FirstOrDefaultAsync(e => e.Sensor!.DeviceName == sensorName && e.EndUserId == endUserId);
            if (existingSensor != null)
                throw new SensorException("Device with this name already exists or does not belong to current end user.");
        }

        private async Task CheckIfSensorForUserExistsAsync(string sensorName, string endUserId)
        {
            var existingSensor = await _dbContext.EndUserSensors
                .Include(e => e.Sensor)
                .FirstOrDefaultAsync(e => e.Sensor!.DeviceName == sensorName && e.EndUserId == endUserId);
            if (existingSensor == null)
                throw new SensorException("Device with this name does not exist or does not belong to current end user.");
        }

        private async Task<Guid> FindSensorByNameAsync(string sensorName, string endUserId)
        {
            await CheckIfSensorForUserExistsAsync(sensorName, endUserId);

            var existingSensor = await _dbContext.Sensors.FirstOrDefaultAsync(s => s.DeviceName == sensorName);
            if (existingSensor == null)
                throw new SensorException("Device with this name does not exist, please check your device name.");

            return existingSensor.Id;
        }
    }
}
