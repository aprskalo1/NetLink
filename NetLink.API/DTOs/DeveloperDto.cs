using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NetLink.API.DTOs;

public class DeveloperDto
{
    [Required]
    public string? DevToken { get; set; }

    [Required]
    public string? Username { get; set; }
}