﻿using System.ComponentModel.DataAnnotations.Schema;

namespace NetLink.API.Models;

public class SensorGroup
{
    public Guid Id { get; init; }

    public string? GroupName { get; init; }

    public DateTime CreatedAt { get; init; } = DateTime.Now;

    public Guid SensorId { get; init; }

    public Sensor? Sensor { get; init; }

    public ICollection<EndUserSensorGroup>? EndUserSensorGroups { get; init; }
}