using System.ComponentModel.DataAnnotations;

namespace NetLink.API.DTOs.Request;

public class RecordedValueRequestDto
{
    [Required] [MaxLength(50)] public string? Value { get; init; }
}