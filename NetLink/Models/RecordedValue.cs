namespace NetLink.Models;

public class RecordedValue(string? value, DateTime recordedAt)
{
    public string? Value { get; set; } = value;
    public DateTime RecordedAt { get; init; } = recordedAt;
}