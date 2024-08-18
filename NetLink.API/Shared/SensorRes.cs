namespace NetLink.API.Shared;

public class SensorRes
{
    public Guid Id { get; init; }
    
    public string? DeviceName { get; init; }
    
    public string? DeviceType { get; init; }
    
    public string? MeasurementUnit { get; init; }
    
    public string? DeviceLocation { get; init; }
    
    public string? DeviceDescription { get; set; }
    
    public DateTime CreatedAt { get; set; }
}