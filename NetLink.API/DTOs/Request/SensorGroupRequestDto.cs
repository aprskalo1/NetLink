using System.ComponentModel.DataAnnotations;

namespace NetLink.API.DTOs.Request;

public class SensorGroupRequestDto
{
    [Required]
    public string? GroupName { get; init; }
}