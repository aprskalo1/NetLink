namespace NetLink.Utilities;

internal static class ApiUrls
{
    public const string BaseUrl = "https://localhost:7260/api";

    public const string AuthUrl = "/Auth/AuthorizeClient?devToken={0}";
    public const string DevTokenValidationUrl = "/Auth/ValidateDeveloper?token={0}";

    public const string EndUserRegistrationUrl = "/EndUsers/RegisterEndUser?devToken={0}";
    public const string EndUserValidationUrl = "/EndUsers/ValidateEndUser?endUserId={0}";
    public const string GetEndUserByIdUrl = "/EndUsers/GetEndUserById?endUserId={0}";
    public const string ListDevelopersEndUsersUrl = "/EndUsers/ListDevelopersEndUsers?devToken={0}";
    public const string EndUserDeactivationUrl = "/EndUsers/DeactivateEndUser?endUserId={0}";
    public const string EndUserReactivationUrl = "/EndUsers/ReactivateEndUser?endUserId={0}";
    public const string EndUserSoftDeletionUrl = "/EndUsers/SoftDeleteEndUser?endUserId={0}";
    public const string EndUserRestorationUrl = "/EndUsers/RestoreEndUser?endUserId={0}";
    public const string AssignSensorsToEndUserUrl = "/EndUsers/AssignSensorsToEndUser?endUserId={0}";
    
    public const string AddSensorUrl = "/Sensors/AddSensor?endUserId={0}";
    public const string GetSensorByNameUrl = "/Sensors/GetSensorByName?deviceName={0}&endUserId={1}";
    public const string GetSensorByIdUrl = "/Sensors/GetSensorById?sensorId={0}&endUserId={1}";
    public const string UpdateSensorUrl = "/Sensors/UpdateSensor?sensorId={0}&endUserId={1}";
    public const string DeleteSensorUrl = "/Sensors/DeleteSensor?sensorId={0}&endUserId={1}";

    public const string AddRecordedValueUrl = "/RecordedValues/AddRecordedValue?sensorName={0}&endUserId={1}";
}