using System.ComponentModel.DataAnnotations;

namespace NetLink.API.Models;

public class RecordedValue
{
    [Key] public Guid Id { get; init; }
    [MaxLength(50)] public string? Value { get; init; }
    public DateTime RecordedAt { get; init; } = DateTime.Now;
    public Guid SensorId { get; set; }
    public Sensor Sensor { get; init; }
}