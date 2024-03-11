namespace NetLink.API.Models
{
    public class EndUser
    {
        public string? Id { get; set; } // Id returned from external login service

        public ICollection<DeveloperUser>? DeveloperUsers { get; set; }
        public ICollection<EndUserSensor>? EndUserSensors { get; set; }
        public ICollection<EndUserSensorGroup>? EndUserSensorGroups { get; set; }
    }
}
