namespace NetLink.Utilities;

public static class ApiUrls
{
    public const string BaseUrl = "https://localhost:7260/api";
    public const string DevTokenValidationUrl = "/Developers/CheckIfTokenExists?token={0}";
    public const string EndUserValidationUrl = "/EndUsers/CheckIfEndUserExists?endUserId={0}";
    public const string AddSensorUrl = "/Sensors/AddSensor?endUserId={0}";
    public const string GetSensorByNameUrl = "/Sensors/GetSensorById?deviceName={0}&endUserId={1}";
    public const string AddRecordedValueUrl = "/RecordedValues/AddRecordedValue?sensorName={0}&endUserId={1}";
}