using System.ComponentModel.DataAnnotations;

namespace NetLink.API.DTOs.Request;

public class GroupRequestDto
{
    public Guid Id { get; init; }
    [Required] [MaxLength(50)] public string? GroupName { get; init; }
}