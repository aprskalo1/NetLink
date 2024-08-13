using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLink.API.Models;

public class EndUser
{
    public string? Id { get; set; } // ID returned from external login service

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? DeletedAt { get; set; }

    public bool Active { get; set; } = true;

    public ICollection<DeveloperUser>? DeveloperUsers { get; set; }

    public ICollection<EndUserSensor>? EndUserSensors { get; set; }

    public ICollection<EndUserSensorGroup>? EndUserSensorGroups { get; set; }
}