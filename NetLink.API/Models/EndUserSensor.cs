using System.ComponentModel.DataAnnotations;

namespace NetLink.API.Models;

public class EndUserSensor
{
    public Guid Id { get; init; }
    [MaxLength(450)] public string? EndUserId { get; init; }
    public EndUser? EndUser { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public Guid SensorId { get; init; }
    public Sensor Sensor { get; init; }
}