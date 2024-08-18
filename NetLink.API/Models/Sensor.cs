﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLink.API.Models;

public class Sensor
{
    [Key]
    public Guid Id { get; init; }

    public string? DeviceName { get; init; }
    
    public string? DeviceType { get; init; }
    
    public string? MeasurementUnit { get; init; }
    
    public string? DeviceLocation { get; init; }
    
    public string? DeviceDescription { get; init; }

    public DateTime CreatedAt { get; init; } = DateTime.Now;

    public ICollection<EndUserSensor>? EndUserSensors { get; init; }
}