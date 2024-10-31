using System.ComponentModel.DataAnnotations;

namespace NetLink.API.DTOs.Request;

public class EndUserRequestDto
{
    [Required] [MaxLength(450)] public string? Id { get; init; } //This ID is ID returned from external login service
    [MaxLength(50)] public string? Username { get; set; }
    [MaxLength(50)] public string? Email { get; set; }
    [MaxLength(50)] public string? FirstName { get; set; }
    [MaxLength(50)] public string? LastName { get; set; }
}