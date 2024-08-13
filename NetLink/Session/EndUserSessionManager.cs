using NetLink.Models;

namespace NetLink.Session;

public interface IEndUserSessionManager
{
    void LogEndUserIn(EndUser endUser);
    string GetLoggedEndUserId();
}

internal class EndUserSessionManager : IEndUserSessionManager
{
    private EndUser? _endUser;

    public void LogEndUserIn(EndUser endUser)
    {
        CheckEndUser(endUser.Id!);
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

    internal void CheckEndUser(string endUserId)
    {
        string tokenCheckEndpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.EndUserValidationUrl, endUserId)}";

        using (var httpClient = new HttpClient())
        {
            try
            {
                var response = httpClient.GetAsync(tokenCheckEndpoint).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("EndUserId invalid, please use another account.");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("An error occurred while validating EndUserId. Please check your network connection and try again.", ex);
            }
        }
    }
}