namespace NetLink.API.Shared;

public class DeveloperRes
{
    public Guid Id { get; init; }

    public string? DevToken { get; init; }

    public string? Username { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? DeletedAt { get; init; }

    public bool Active { get; init; } = true;
}