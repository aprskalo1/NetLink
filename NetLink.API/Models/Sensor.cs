using System.ComponentModel.DataAnnotations;

namespace NetLink.API.Models
{
    public class Sensor
    {
        [Key]
        public Guid Id { get; set; }
        public string? DeviceName { get; set; }
        public string? RecordedValue { get; set; }

        public ICollection<EndUserSensor>? EndUserSensors { get; set; }
    }
}
