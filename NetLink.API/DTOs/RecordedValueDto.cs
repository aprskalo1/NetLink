using System.ComponentModel.DataAnnotations;

namespace NetLink.API.DTOs;

public class RecordedValueDto
{
    [Required] 
    public string? Value { get; set; }
}