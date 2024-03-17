using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLink.API.Models
{
    public class RecordedValue
    {
        [Key]
        public Guid Id { get; set; }

        public string? Value { get; set; }

        public DateTime RecordedAt { get; set; } = DateTime.Now;

        public Guid SensorId { get; set; }

        public Sensor? Sensor { get; set; }
    }
}
