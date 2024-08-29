using NetLink.Services;

namespace NetLink.Models;

public class Sensor(string? deviceName, string? deviceType, string? measurementUnit, string? deviceLocation, string? deviceDescription)
{
    public string? DeviceName { get; init; } = deviceName;

    public string? DeviceType { get; init; } = deviceType;

    public string? MeasurementUnit { get; init; } = measurementUnit;

    public string? DeviceLocation { get; init; } = deviceLocation;

    public string? DeviceDescription { get; init; } = deviceDescription;

    public RecordedValue RecordValue(string value, IRecordedValueService recordedValueService)
    {
        if (string.IsNullOrEmpty(DeviceName))
        {
            throw new Exception("Device name is required.");
        }

        var recordedValue = new RecordedValue(value);
        return recordedValueService.RecordValue(DeviceName!, recordedValue);
    }
}