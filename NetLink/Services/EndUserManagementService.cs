using System.Net.Http.Json;
using NetLink.Models;
using NetLink.Models.DTOs;
using NetLink.Session;
using NetLink.Utilities;

namespace NetLink.Services;

public interface IEndUserManagementService
{
    Task<string> RegisterEndUserAsync(EndUser endUser);
    Task<EndUser> GetEndUserByIdAsync(string endUserId);
    Task ValidateEndUserAsync(string endUserId);
    Task<List<EndUser>> ListDevelopersEndUsersAsync();
    Task DeactivateEndUserAsync(string endUserId);
    Task ReactivateEndUserAsync(string endUserId);
    Task SoftDeleteEndUserAsync(string endUserId);
    Task RestoreEndUserAsync(string endUserId);
    Task AssignSensorsToEndUserAsync(List<Guid> sensorIds, string endUserId);
}

internal class EndUserManagementService(IDeveloperSessionManager developerSessionManager) : IEndUserManagementService
{
    public async Task<string> RegisterEndUserAsync(EndUser endUser)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.EndUserRegistrationUrl, developerSessionManager.GetDevToken())}";
        var response = await SendRequestAsync<string>(HttpMethod.Post, endpoint, endUser);
        return response ?? string.Empty;
    }

    public async Task<EndUser> GetEndUserByIdAsync(string endUserId)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.GetEndUserByIdUrl, endUserId)}";
        var response = await SendRequestAsync<EndUser>(HttpMethod.Get, endpoint);
        return response ?? throw new Exception("End user not found.");
    }

    public async Task ValidateEndUserAsync(string endUserId)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.EndUserValidationUrl, endUserId)}";
        await SendRequestAsync(HttpMethod.Get, endpoint);
    }

    public async Task<List<EndUser>> ListDevelopersEndUsersAsync()
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.ListDevelopersEndUsersUrl, developerSessionManager.GetDevToken())}";
        var response = await SendRequestAsync<List<EndUser>>(HttpMethod.Get, endpoint);
        return response ?? [];
    }

    public async Task DeactivateEndUserAsync(string endUserId)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.EndUserDeactivationUrl, endUserId)}";
        await SendRequestAsync(HttpMethod.Patch, endpoint);
    }

    public async Task ReactivateEndUserAsync(string endUserId)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.EndUserReactivationUrl, endUserId)}";
        await SendRequestAsync(HttpMethod.Patch, endpoint);
    }

    public async Task SoftDeleteEndUserAsync(string endUserId)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.EndUserSoftDeletionUrl, endUserId)}";
        await SendRequestAsync(HttpMethod.Patch, endpoint);
    }

    public async Task RestoreEndUserAsync(string endUserId)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.EndUserRestorationUrl, endUserId)}";
        await SendRequestAsync(HttpMethod.Patch, endpoint);
    }

    public async Task AssignSensorsToEndUserAsync(List<Guid> sensorIds, string endUserId)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.AssignSensorsToEndUserUrl, endUserId)}";
        await SendRequestAsync(HttpMethod.Post, endpoint, sensorIds);
    }

    private async Task SendRequestAsync(HttpMethod method, string endpoint, object? content = null)
    {
        var httpClient = await developerSessionManager.GetAuthenticatedHttpClientAsync();
        var requestMessage = new HttpRequestMessage(method, endpoint);

        if (content != null)
        {
            requestMessage.Content = JsonContent.Create(content);
        }

        try
        {
            var response = await httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ErrorRes>();
                throw new HttpRequestException(errorResponse?.Message ?? "An unknown error occurred.", null, response.StatusCode);
            }
        }
        catch (Exception ex) when (ex is not HttpRequestException)
        {
            throw new HttpRequestException("An error occurred while processing the request.", ex);
        }
    }

    private async Task<T?> SendRequestAsync<T>(HttpMethod method, string endpoint, object? content = null)
    {
        var httpClient = await developerSessionManager.GetAuthenticatedHttpClientAsync();
        var requestMessage = new HttpRequestMessage(method, endpoint);

        if (content != null)
        {
            requestMessage.Content = JsonContent.Create(content);
        }

        try
        {
            var response = await httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                if (response.Content.Headers.ContentLength == 0)
                {
                    return default;
                }

                if (typeof(T) == typeof(string))
                    return (T)(object)await response.Content.ReadAsStringAsync();

                return await response.Content.ReadFromJsonAsync<T>();
            }

            var errorResponse = await response.Content.ReadFromJsonAsync<ErrorRes>();
            throw new HttpRequestException(errorResponse?.Message ?? "An unknown error occurred.", null, response.StatusCode);
        }
        catch (Exception ex) when (ex is not HttpRequestException)
        {
            throw new HttpRequestException("An error occurred while processing the request.", ex);
        }
    }
}