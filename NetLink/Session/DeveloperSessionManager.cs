using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using NetLink.Models.DTOs;
using NetLink.Utilities;

namespace NetLink.Session;

internal interface IDeveloperSessionManager
{
    Task<HttpClient> GetAuthenticatedHttpClientAsync();
}

internal class DeveloperSessionManager : IDeveloperSessionManager
{
    private readonly HttpClient _httpClient = new();
    private readonly string? _devToken;
    private string? _jwtToken;
    private DateTime _tokenExpiration;

    public DeveloperSessionManager(string? devToken)
    {
        ValidateDevTokenAsync(devToken).GetAwaiter().GetResult();
        _devToken = devToken;
    }

    private async Task ValidateDevTokenAsync(string? devToken)
    {
        var devTokenValidationUrl = string.Format($"{ApiUrls.BaseUrl}{ApiUrls.DevTokenValidationUrl}", devToken);
        using var response = await _httpClient.GetAsync(devTokenValidationUrl);

        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<ErrorRes>();
            throw new Exception($"DevToken validation failed: {errorResponse?.Message}");
        }
    }

    public async Task<HttpClient> GetAuthenticatedHttpClientAsync()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetJwtTokenAsync());
        return _httpClient;
    }

    private async Task RetrieveJwtTokenAsync()
    {
        var authUrl = string.Format($"{ApiUrls.BaseUrl}{ApiUrls.AuthUrl}", _devToken);

        using var response = await _httpClient.PostAsync(authUrl, null);
        response.EnsureSuccessStatusCode();

        var token = await response.Content.ReadAsStringAsync();
        var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

        _jwtToken = token;
        _tokenExpiration = jwtSecurityToken.ValidTo;
    }

    private async Task<string> GetJwtTokenAsync()
    {
        if (_jwtToken == null || _tokenExpiration < DateTime.UtcNow)
            await RetrieveJwtTokenAsync();
        return _jwtToken!;
    }
}