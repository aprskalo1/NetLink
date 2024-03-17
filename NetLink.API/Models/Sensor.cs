using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLink.API.Models
{
    public class Sensor
    {
        [Key]
        public Guid Id { get; set; }

        public string? DeviceName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<EndUserSensor>? EndUserSensors { get; set; }
    }
}
