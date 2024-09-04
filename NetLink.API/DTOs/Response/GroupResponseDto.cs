namespace NetLink.API.DTOs.Response;

public class GroupResponseDto
{
    public Guid Id { get; init; }
    public string? GroupName { get; init; }
    public DateTime CreatedAt { get; init; }
}