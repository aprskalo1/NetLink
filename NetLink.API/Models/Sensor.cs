using System.ComponentModel.DataAnnotations;

namespace NetLink.API.Models;

public class Sensor
{
    [Key] public Guid Id { get; init; }
    [MaxLength(50)] public string? DeviceName { get; init; }
    [MaxLength(50)] public string? DeviceType { get; init; }
    [MaxLength(50)] public string? MeasurementUnit { get; init; }
    [MaxLength(150)] public string? DeviceLocation { get; init; }
    [MaxLength(450)] public string? DeviceDescription { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public EndUserSensor EndUserSensors { get; init; }
    public ICollection<SensorGroup> SensorGroups { get; init; }
}