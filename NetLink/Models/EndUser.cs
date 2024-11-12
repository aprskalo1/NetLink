namespace NetLink.Models;

public class EndUser(string? id)
{
    public string? Id { get; } = id;
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime? DeletedAt { get; set; }
    public bool Active { get; set; }
}