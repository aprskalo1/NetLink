using NetLink.Models;
using NetLink.Utilities;

namespace NetLink.Session;

public interface IEndUserSessionManager
{
    Task LogInEndUserAsync(EndUser endUser);
    string GetLoggedEndUserId();
}

internal class EndUserSessionManager(IDeveloperSessionManager developerSessionManager) : IEndUserSessionManager
{
    private EndUser? _endUser;

    public async Task LogInEndUserAsync(EndUser endUser)
    {
        await CheckEndUserAsync(endUser.Id!);
        _endUser = endUser;
    }

    public string GetLoggedEndUserId()
    {
        if (_endUser == null)
        {
            throw new Exception("EndUser is not logged in. Please log in first.");
        }

        return _endUser.Id!;
    }

    private async Task CheckEndUserAsync(string endUserId)
    {
        var tokenCheckEndpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.EndUserValidationUrl, endUserId)}";

        using var httpClient = await developerSessionManager.GetAuthenticatedHttpClientAsync();
        var response = await httpClient.GetAsync(tokenCheckEndpoint);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();

            throw response.StatusCode switch
            {
                System.Net.HttpStatusCode.NotFound => new Exception($"EndUser does not exist. Server message: {errorMessage}"),
                System.Net.HttpStatusCode.BadRequest => new Exception($"Invalid EndUser. Server message: {errorMessage}"),
                System.Net.HttpStatusCode.InternalServerError => new Exception($"Server error occurred. Message: {errorMessage}"),
                _ => new Exception($"Unexpected error. Status code: {(int)response.StatusCode}, Message: {errorMessage}")
            };
        }
    }
}