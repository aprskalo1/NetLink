using System.ComponentModel.DataAnnotations;

namespace NetLink.API.DTOs.Request;

public class EndUserRequestDto
{
    [Required] [MaxLength(450)] public string? Id { get; init; } //This ID is ID returned from external login service
}