using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLink.API.Models;

public class DeveloperUser
{
    [Key]
    public Guid Id { get; init; }

    [Required]
    public Guid DeveloperId { get; init; }

    [Required]
    public string? EndUserId { get; init; }

    public DateTime CreatedAt { get; init; } = DateTime.Now;

    public Developer? Developer { get; init; }

    public EndUser? EndUser { get; init; }
}