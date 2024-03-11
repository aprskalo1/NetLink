namespace NetLink.API.Models
{
    public class SensorGroup
    {
        public Guid Id { get; set; }
        public string? GroupName { get; set; }

        public Guid SensorId { get; set; }
        public Sensor? Sensor { get; set; }

        public ICollection<EndUserSensorGroup>? EndUserSensorGroups { get; set; }
    }
}
