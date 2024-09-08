namespace NetLink.Models;

public class Sensor(
    string deviceName,
    string deviceType,
    string measurementUnit,
    string? deviceLocation = null,
    string? deviceDescription = null,
    Guid? id = null)
{
    public Guid? Id { get; init; } = id;
    public string DeviceName { get; } = deviceName;

    public string DeviceType { get; init; } = deviceType;

    public string MeasurementUnit { get; init; } = measurementUnit;

    public string? DeviceLocation { get; init; } = deviceLocation;

    public string? DeviceDescription { get; init; } = deviceDescription;
}