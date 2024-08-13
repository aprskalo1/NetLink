namespace NetLink.Session;

internal interface IDeveloperSessionManager
{
    void AddDevTokenAuthentication(string devToken);
}

internal class DeveloperSessionManager : IDeveloperSessionManager
{
    private string? _devToken; //consider using session for this later if it is a good practice to use it

    public string? DevToken
    {
        get => _devToken;
    }

    public void AddDevTokenAuthentication(string devToken)
    {
        CheckDevToken(devToken);
        _devToken = devToken;
    }

    internal void CheckDevToken(string devToken)
    {
        string tokenCheckEndpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.DevTokenValidationUrl, devToken)}";

        using (var httpClient = new HttpClient())
        {
            try
            {
                var response = httpClient.GetAsync(tokenCheckEndpoint).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(
                        "DevToken is invalid or your account is deactivated, please check DevToken in your configuration file or account status.");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(
                    "An error occurred while validating DevToken. Please check your network connection and try again.",
                    ex);
            }
        }
    }
}