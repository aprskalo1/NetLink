using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLink.API.Models;

public class EndUser
{
    public string? Id { get; init; } // ID returned from external login service

    public DateTime CreatedAt { get; init; } = DateTime.Now;

    public DateTime? DeletedAt { get; init; }

    public bool Active { get; init; } = true;

    public ICollection<DeveloperUser>? DeveloperUsers { get; init; }

    public ICollection<EndUserSensor>? EndUserSensors { get; init; }

    public ICollection<EndUserSensorGroup>? EndUserSensorGroups { get; init; }
}