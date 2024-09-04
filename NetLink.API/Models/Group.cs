using System.ComponentModel.DataAnnotations;

namespace NetLink.API.Models;

public class Group
{
    [Key] public Guid Id { get; init; }
    [MaxLength(50)] public string? GroupName { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public ICollection<EndUserGroup>? EndUserGroups { get; init; }
    public ICollection<SensorGroup>? SensorGroups { get; init; }
}