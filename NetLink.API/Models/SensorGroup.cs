using System.ComponentModel.DataAnnotations;

namespace NetLink.API.Models;

public class SensorGroup
{
    [Key] public Guid Id { get; init; }
    public Guid SensorId { get; init; }
    public Sensor Sensor { get; init; }
    public Guid GroupId { get; init; }
    public Group? Group { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;
}