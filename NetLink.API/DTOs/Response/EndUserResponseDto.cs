namespace NetLink.API.DTOs.Response;

public class EndUserResponseDto
{
    public string? Id { get; init; } // ID returned from external login service

    public DateTime CreatedAt { get; init; }

    public DateTime? DeletedAt { get; init; }

    public bool Active { get; init; }
}