using NetLink.Helpers;
using NetLink.Models;
using NetLink.Session;
using NetLink.Utilities;

namespace NetLink.Services;

public interface IGroupingService
{
    Task<Guid> CreateGroupAsync(Group group, string? endUserId = null);
    Task AddSensorToGroupAsync(Guid groupId, Guid sensorId, string? endUserId = null);
    Task RemoveSensorFromGroupAsync(Guid groupId, Guid sensorId, string? endUserId = null);
    Task DeleteGroupAsync(Guid groupId, string? endUserId = null);
    Task<List<Group>> GetEndUserGroupsAsync(string? endUserId = null);
    Task<Group> GetGroupByIdAsync(Guid groupId, string? endUserId = null);
    Task UpdateGroupAsync(Group group, string? endUserId = null);
}

internal class GroupingService(IEndUserSessionManager endUserSessionManager, IDeveloperSessionManager developerSessionManager)
    : HttpHelper(developerSessionManager), IGroupingService
{
    public async Task<Guid> CreateGroupAsync(Group group, string? endUserId = null)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.CreateGroupUrl, GetEffectiveUserId(endUserId))}";
        var response = await SendRequestAsync<Guid>(HttpMethod.Post, endpoint, group);
        return response;
    }

    public async Task AddSensorToGroupAsync(Guid groupId, Guid sensorId, string? endUserId = null)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.AddSensorToGroupUrl, groupId, sensorId, GetEffectiveUserId(endUserId))}";
        await SendRequestAsync(HttpMethod.Post, endpoint);
    }

    public Task RemoveSensorFromGroupAsync(Guid groupId, Guid sensorId, string? endUserId = null)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.RemoveSensorFromGroupUrl, groupId, sensorId, GetEffectiveUserId(endUserId))}";
        return SendRequestAsync(HttpMethod.Post, endpoint);
    }

    public Task DeleteGroupAsync(Guid groupId, string? endUserId = null)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.DeleteGroupUrl, groupId, GetEffectiveUserId(endUserId))}";
        return SendRequestAsync(HttpMethod.Delete, endpoint);
    }

    public async Task<List<Group>> GetEndUserGroupsAsync(string? endUserId = null)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.GetEndUserGroups, GetEffectiveUserId(endUserId))}";
        var response = await SendRequestAsync<List<Group>>(HttpMethod.Get, endpoint);
        return response ?? [];
    }

    public async Task<Group> GetGroupByIdAsync(Guid groupId, string? endUserId = null)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.GetGroupByIdUrl, groupId, GetEffectiveUserId(endUserId))}";
        return await SendRequestAsync<Group>(HttpMethod.Get, endpoint) ?? throw new Exception("Group not found.");
    }

    public Task UpdateGroupAsync(Group group, string? endUserId = null)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.UpdateGroupUrl, group.Id, GetEffectiveUserId(endUserId))}";
        return SendRequestAsync(HttpMethod.Put, endpoint, group);
    }

    private string GetEffectiveUserId(string? userId)
    {
        return userId ?? endUserSessionManager.GetLoggedEndUserId();
    }
}