using System.ComponentModel.DataAnnotations;

namespace NetLink.API.DTOs.Request;

public class RecordedValueRequestDto
{
    [Required] public double Value { get; init; }
}