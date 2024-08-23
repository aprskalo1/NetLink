namespace NetLink.Models;

public abstract class EndUser(string? id)
{
    public string? Id { get; set; } = id;
}