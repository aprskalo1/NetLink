namespace NetLink.Models;

public class Group(string groupName)
{
    public Guid Id { get; init; }
    public string GroupName { get; init; } = groupName;
    public DateTime CreatedAt { get; init; }
}