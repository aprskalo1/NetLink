using System.ComponentModel.DataAnnotations;

namespace NetLink.API.DTOs;

public class SensorDto
{
    [Required]
    public string? DeviceName { get; set; }
}