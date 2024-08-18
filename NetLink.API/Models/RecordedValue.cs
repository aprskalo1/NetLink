using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLink.API.Models;

public class RecordedValue
{
    [Key]
    public Guid Id { get; init; }

    public string? Value { get; init; }

    public DateTime RecordedAt { get; init; } = DateTime.Now;

    public Guid SensorId { get; set; }

    public Sensor? Sensor { get; init; }
}