namespace NetLink.Models;

public class RecordedValue
{
    public RecordedValue(string? value)
    {
        Value = value;
    }

    public string? Value { get; set; }
}