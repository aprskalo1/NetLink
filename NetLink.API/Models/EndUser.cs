﻿using System.ComponentModel.DataAnnotations;

namespace NetLink.API.Models;

public class EndUser
{
    [MaxLength(450)] public string? Id { get; init; } // ID returned from external login service

    [MaxLength(50)] public string? Username { get; set; }
    [MaxLength(50)] public string? Email { get; set; }
    [MaxLength(50)] public string? FirstName { get; set; }
    [MaxLength(50)] public string? LastName { get; set; }

    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public DateTime? DeletedAt { get; set; }
    public bool Active { get; set; } = true;
}