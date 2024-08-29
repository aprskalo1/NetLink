namespace NetLink.Models;

public class EndUser(string? id)
{
    public string? Id { get; } = id;

    public DateTime CreatedAt { get; init; }

    public DateTime? DeletedAt { get; set; }

    public bool Active { get; set; }
}