using System.ComponentModel.DataAnnotations;

namespace NetLink.API.Shared.DTOs;

public class RecordedValueDto
{
    [Required] 
    public string? Value { get; init; }
}