using System.ComponentModel.DataAnnotations;

namespace NetLink.API.Shared.DTOs;

public class EndUserDto
{
    [Required]
    public string? Id { get; init; } //This ID is ID returned from external login service
}