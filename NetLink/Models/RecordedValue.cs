namespace NetLink.Models;

public class RecordedValue(double value)
{
    public double Value { get; } = value;
    public DateTime RecordedAt { get; init; }
}