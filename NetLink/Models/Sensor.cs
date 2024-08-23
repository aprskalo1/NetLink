using NetLink.Services;

namespace NetLink.Models;

public class Sensor(string? deviceName)
{
    private string? DeviceName { get; set; } = deviceName;

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