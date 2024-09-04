using System.ComponentModel.DataAnnotations;

namespace NetLink.API.DTOs.Request;

public class DeveloperRequestDto
{
    [Required] [MaxLength(50)] public string? Username { get; init; }
}