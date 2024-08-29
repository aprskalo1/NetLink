using System.Net;
using System.Net.Http.Json;
using NetLink.Models;
using NetLink.Models.DTOs;
using NetLink.Utilities;

namespace NetLink.Session;

public interface IEndUserSessionManager
{
    Task LogInEndUserAsync(EndUser endUser);
    void LogOutEndUser();
    string GetLoggedEndUserId();
}

internal class EndUserSessionManager(IDeveloperSessionManager developerSessionManager) : IEndUserSessionManager
{
    private EndUser? _endUser;

    public async Task LogInEndUserAsync(EndUser endUser)
    {
        await ValidateEndUserAsync(endUser.Id!);
        _endUser = endUser;
    }

    public void LogOutEndUser()
    {
        _endUser = null;
    }

    public string GetLoggedEndUserId()
    {
        if (_endUser == null)
        {
            throw new Exception("EndUser is not logged in. Please log in first.");
        }

        return _endUser.Id!;
    }

    private async Task ValidateEndUserAsync(string endUserId)
    {
        var tokenCheckEndpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.EndUserValidationUrl, endUserId)}";

        try
        {
            var httpClient = await developerSessionManager.GetAuthenticatedHttpClientAsync();
            var response = await httpClient.GetAsync(tokenCheckEndpoint);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadFromJsonAsync<ErrorRes>();

                throw response.StatusCode switch
                {
                    HttpStatusCode.NotFound => new Exception($"EndUser does not exist. Server message: {errorMessage?.Message}"),
                    HttpStatusCode.BadRequest => new Exception($"Invalid EndUser. Server message: {errorMessage?.Message}"),
                    HttpStatusCode.InternalServerError => new Exception($"Server error occurred. Message: {errorMessage?.Message}"),
                    _ => new Exception($"Unexpected error. Status code: {(int)response.StatusCode}, Message: {errorMessage?.Message}")
                };
            }
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("EndUser validation failed: NetLink server is unreachable, if the problem persists, please contact support.", ex);
        }
    }
}