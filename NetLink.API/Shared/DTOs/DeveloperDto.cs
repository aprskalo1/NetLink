using System.ComponentModel.DataAnnotations;

namespace NetLink.API.Shared.DTOs;

public class DeveloperDto
{
    [Required]
    public string? Username { get; init; }
}