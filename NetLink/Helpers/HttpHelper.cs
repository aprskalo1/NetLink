using System.Net.Http.Json;
using NetLink.Models.DTOs;
using NetLink.Session;

namespace NetLink.Helpers;

public abstract class HttpHelper
{
    private readonly IDeveloperSessionManager _developerSessionManager;

    internal HttpHelper(IDeveloperSessionManager developerSessionManager)
    {
        _developerSessionManager = developerSessionManager;
    }

    internal async Task SendRequestAsync(HttpMethod method, string endpoint, object? content = null)
    {
        var httpClient = await _developerSessionManager.GetAuthenticatedHttpClientAsync();
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

    internal async Task<T?> SendRequestAsync<T>(HttpMethod method, string endpoint, object? content = null)
    {
        var httpClient = await _developerSessionManager.GetAuthenticatedHttpClientAsync();
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