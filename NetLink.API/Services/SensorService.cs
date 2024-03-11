using AutoMapper;
using NetLink.API.Data;
using NetLink.API.DTOs;
using NetLink.API.Exceptions;
using NetLink.API.Models;

namespace NetLink.API.Services
{

    public interface ISensorService
    {
        Task<Guid> AddSensorAsync(SensorDTO sensorDTO);
        Task<SensorDTO> GetSensorByIdAsync(Guid id);
    }

    public class SensorService : ISensorService
    {
        private readonly IMapper _mapper;
        private readonly NetLinkDbContext _dbContext;

        public SensorService(IMapper mapper, NetLinkDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<Guid> AddSensorAsync(SensorDTO sensorDTO)
        {
            var sensor = _mapper.Map<Sensor>(sensorDTO);
            _dbContext.Sensors.Add(sensor);
            await _dbContext.SaveChangesAsync();
            return sensor.Id;
        }

        public async Task<SensorDTO> GetSensorByIdAsync(Guid id)
        {
            var sensor = await _dbContext.Sensors.FindAsync(id);
            if (sensor == null)
                throw new NotFoundException();
            return _mapper.Map<SensorDTO>(sensor);
        }
    }
}
