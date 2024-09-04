namespace NetLink.API.DTOs.Response;

public class SensorResponseDto
{
    public Guid Id { get; init; }
    public string? DeviceName { get; init; }
    public string? DeviceType { get; init; }
    public string? MeasurementUnit { get; init; }
    public string? DeviceLocation { get; init; }
    public string? DeviceDescription { get; init; }
    public DateTime CreatedAt { get; init; }
}