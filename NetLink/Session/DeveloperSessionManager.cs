using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using NetLink.Utilities;

namespace NetLink.Session;

internal interface IDeveloperSessionManager
{
    Task<HttpClient> GetAuthenticatedHttpClientAsync();
}

internal class DeveloperSessionManager(string devToken) : IDeveloperSessionManager
{
    private readonly HttpClient _httpClient = new();
    private string? _jwtToken;
    private DateTime _tokenExpiration;

    public async Task<HttpClient> GetAuthenticatedHttpClientAsync()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetJwtTokenAsync());
        return _httpClient;
    }
    
    private async Task RetrieveJwtTokenAsync()
    {
        var authUrl = string.Format($"{ApiUrls.BaseUrl}{ApiUrls.AuthUrl}", devToken);
        var response = await _httpClient.PostAsync(authUrl, null);
        response.EnsureSuccessStatusCode();

        var token = await response.Content.ReadAsStringAsync();
        var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

        _jwtToken = token;
        _tokenExpiration = jwtSecurityToken.ValidTo;
    }

    private async Task<string> GetJwtTokenAsync()
    {
        if (_jwtToken == null || _tokenExpiration < DateTime.UtcNow)
        {
            await RetrieveJwtTokenAsync();
        }

        return _jwtToken!;
    }

}