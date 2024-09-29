namespace NetLink.Utilities;

internal static class ApiUrls
{
    //public const string BaseUrl = "https://localhost:7260/api";
    public const string BaseUrl = "https://netlink-solution.com/api";

    // public const string SignalRUrl = "https://localhost:7260/sensorHub";
    public const string SignalRUrl = "https://netlink-solution.com/sensorHub";

    public const string AuthUrl = "/Auth/AuthorizeClient?devToken={0}";
    public const string DevTokenValidationUrl = "/Auth/ValidateDeveloper?devToken={0}";

    public const string EndUserRegistrationUrl = "/EndUsers/RegisterEndUser?devToken={0}";
    public const string EndUserValidationUrl = "/EndUsers/ValidateEndUser?endUserId={0}";
    public const string GetEndUserByIdUrl = "/EndUsers/GetEndUserById?endUserId={0}&devToken={1}";
    public const string ListDevelopersEndUsersUrl = "/EndUsers/ListDevelopersEndUsers?devToken={0}";
    public const string EndUserDeactivationUrl = "/EndUsers/DeactivateEndUser?endUserId={0}";
    public const string EndUserReactivationUrl = "/EndUsers/ReactivateEndUser?endUserId={0}";
    public const string EndUserSoftDeletionUrl = "/EndUsers/SoftDeleteEndUser?endUserId={0}";
    public const string EndUserRestorationUrl = "/EndUsers/RestoreEndUser?endUserId={0}";
    public const string AssignSensorsToEndUserUrl = "/EndUsers/AssignSensorsToEndUser?endUserId={0}";
    public const string ListEndUserSensorsUrl = "/EndUsers/ListEndUserSensors?endUserId={0}";
    
    public const string AddSensorUrl = "/Sensors/AddSensor?endUserId={0}";
    public const string GetSensorByNameUrl = "/Sensors/GetSensorByName?deviceName={0}&endUserId={1}";
    public const string GetSensorByIdUrl = "/Sensors/GetSensorById?sensorId={0}&endUserId={1}";
    public const string UpdateSensorUrl = "/Sensors/UpdateSensor?sensorId={0}&endUserId={1}";
    public const string DeleteSensorUrl = "/Sensors/DeleteSensor?sensorId={0}&endUserId={1}";
    
    public const string CreateGroupUrl = "/Grouping/CreateGroup?endUserId={0}";
    public const string AddSensorToGroupUrl = "/Grouping/AddSensorToGroup?groupId={0}&sensorId={1}&endUserId={2}";
    public const string RemoveSensorFromGroupUrl = "/Grouping/RemoveSensorFromGroup?groupId={0}&sensorId={1}&endUserId={2}";
    public const string DeleteGroupUrl = "/Grouping/DeleteGroup?groupId={0}&endUserId={1}";
    public const string GetEndUserGroups = "/Grouping/GetEndUserGroups?endUserId={0}";
    public const string GetGroupByIdUrl = "/Grouping/GetGroupById?groupId={0}&endUserId={1}";
    public const string UpdateGroupUrl = "/Grouping/UpdateGroup?groupId={0}&endUserId={1}";

    public const string RecordValueBySensorNameUrl = "/RecordedValues/RecordValueBySensorName?sensorName={0}&endUserId={1}";
    public const string RecordValueBySensorIdUrl = "/RecordedValues/RecordValueBySensorId?sensorId={0}";
    public const string GetRecordedValuesUrl = "/RecordedValues/GetRecordedValues?sensorId={0}&endUserId={1}&quantity={2}&isAscending={3}&startDate={4}&endDate={5}";
}