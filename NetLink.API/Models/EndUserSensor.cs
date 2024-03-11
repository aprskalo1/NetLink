namespace NetLink.API.Models
{
    public class EndUserSensor
    {
        public Guid Id { get; set; }
        public string? EndUserId { get; set; }
        public EndUser? EndUser { get; set; }

        public Guid SensorId { get; set; }
        public Sensor? Sensor { get; set; }
    }
}
