using System.ComponentModel.DataAnnotations;

namespace NetLink.API.Models;

public class Developer
{
    [Key]
    public Guid Id { get; init; }

    public string? DevToken { get; init; }

    public string? Username { get; init; }

    public DateTime CreatedAt { get; init; } = DateTime.Now;

    public DateTime? DeletedAt { get; init; }

    public bool Active { get; init; } = true;

    public ICollection<DeveloperUser>? DeveloperUsers { get; init; }
}