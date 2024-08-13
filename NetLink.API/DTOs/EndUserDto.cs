using System.ComponentModel.DataAnnotations;

namespace NetLink.API.DTOs;

public class EndUserDto
{
    [Required]
    public string? Id { get; set; } //This ID is ID returned from external login service
}