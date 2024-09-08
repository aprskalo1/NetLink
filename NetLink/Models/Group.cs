namespace NetLink.Models;

public class Group(string groupName, Guid? id = null)
{
    public Guid? Id { get; init; } = id;
    public string GroupName { get; init; } = groupName;
    public DateTime CreatedAt { get; init; }
}