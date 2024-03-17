using System.ComponentModel.DataAnnotations.Schema;

namespace NetLink.API.Models
{
    public class EndUserSensorGroup
    {
        public Guid Id { get; set; }
        public string? EndUserId { get; set; }

        public EndUser? EndUser { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid SensorGroupId { get; set; }

        public SensorGroup? SensorGroup { get; set; }
    }
}
