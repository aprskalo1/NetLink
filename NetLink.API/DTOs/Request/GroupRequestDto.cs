using System.ComponentModel.DataAnnotations;

namespace NetLink.API.DTOs.Request;

public class GroupRequestDto
{
    [Required] [MaxLength(50)] public string? GroupName { get; init; }
}