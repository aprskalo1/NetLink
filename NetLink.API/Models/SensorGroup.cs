using System.ComponentModel.DataAnnotations;

namespace NetLink.API.Models;

public class SensorGroup
{
    [Key]
    public Guid Id { get; init; }

    public string? GroupName { get; init; }

    public DateTime CreatedAt { get; init; } = DateTime.Now;

    public Guid? SensorId { get; init; }

    public Sensor? Sensor { get; init; }

    public ICollection<EndUserSensorGroup>? EndUserSensorGroups { get; init; }
}