using System.ComponentModel.DataAnnotations.Schema;

namespace NetLink.API.Models;

public class EndUserSensorGroup
{
    public Guid Id { get; init; }

    public string? EndUserId { get; init; }

    public EndUser? EndUser { get; init; }

    public DateTime CreatedAt { get; init; } = DateTime.Now;

    public Guid SensorGroupId { get; init; }

    public SensorGroup? SensorGroup { get; init; }
}