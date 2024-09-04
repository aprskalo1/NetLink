using System.ComponentModel.DataAnnotations;

namespace NetLink.API.Models;

public class Developer
{
    [Key] public Guid Id { get; init; }
    [MaxLength(250)] public string? DevToken { get; set; }
    [MaxLength(50)] public string? Username { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public DateTime? DeletedAt { get; set; }
    public bool Active { get; set; } = true;
}