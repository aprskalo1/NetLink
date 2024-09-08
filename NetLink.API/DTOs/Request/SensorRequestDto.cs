using System.ComponentModel.DataAnnotations;

namespace NetLink.API.DTOs.Request;

public class SensorRequestDto
{
    public Guid Id { get; init; }
    [Required] [MaxLength(50)] public string? DeviceName { get; init; }
    [Required] [MaxLength(50)] public string? DeviceType { get; init; }
    [Required] [MaxLength(50)] public string? MeasurementUnit { get; init; }
    [MaxLength(150)] public string? DeviceLocation { get; init; }
    [MaxLength(450)] public string? DeviceDescription { get; init; }
}