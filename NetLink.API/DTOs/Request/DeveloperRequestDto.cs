using System.ComponentModel.DataAnnotations;

namespace NetLink.API.DTOs.Request;

public class DeveloperRequestDto
{
    [Required]
    public string? Username { get; init; }
}