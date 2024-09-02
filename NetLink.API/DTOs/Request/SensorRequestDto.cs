using System.ComponentModel.DataAnnotations;

namespace NetLink.API.DTOs.Request;

public class SensorRequestDto
{
    [Required]
    public string? DeviceName { get; init; }
    
    [Required]
    public string? DeviceType { get; init; }
    
    [Required]
    public string? MeasurementUnit { get; init; }
    
    public string? DeviceLocation { get; init; }
    
    public string? DeviceDescription { get; init; }
}