using System.ComponentModel.DataAnnotations;

namespace NetLink.API.DTOs.Request;

public class EndUserRequestDto
{
    [Required]
    public string? Id { get; init; } //This ID is ID returned from external login service
}