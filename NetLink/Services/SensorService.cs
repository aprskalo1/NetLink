using System.Net.Http.Json;
using NetLink.Models;
using NetLink.Models.DTOs;
using NetLink.Session;
using NetLink.Utilities;

namespace NetLink.Services;

public interface ISensorService
{
    public Task<Guid> AddSensorAsync(Sensor sensor);
    //public Sensor GetSensorByName(string deviceName);
}

internal class SensorService(IDeveloperSessionManager developerSessionManager, IEndUserSessionManager endUserSessionManager) : ISensorService
{
    public async Task<Guid> AddSensorAsync(Sensor sensor)
    {
        var endUserId = endUserSessionManager.GetLoggedEndUserId();
        var addSensorEndpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.AddSensorUrl, endUserId)}";

        try
        {
            var httpClient = await developerSessionManager.GetAuthenticatedHttpClientAsync();
            var response = await httpClient.PostAsJsonAsync(addSensorEndpoint, sensor);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadFromJsonAsync<ErrorRes>();
                throw new Exception($"An error occurred while adding the sensor: {errorMessage?.Message}");
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var guidString = responseBody.Trim('"');

            return Guid.Parse(guidString);
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Something went wrong. Please check your network connection and try again.", ex);
        }
    }

    // public Sensor GetSensorByName(string deviceName)
    // {
    //     string endUserId = _endUserSessionManager.GetLoggedEndUserId();
    //     string getSensorByNameEndpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.GetSensorByNameUrl, deviceName, endUserId)}";
    //
    //     using (var httpClient = new HttpClient())
    //     {
    //         try
    //         {
    //             HttpResponseMessage response = httpClient.GetAsync(getSensorByNameEndpoint).Result;
    //             response.EnsureSuccessStatusCode();
    //
    //             var responseBody = response.Content.ReadAsStringAsync().Result;
    //             Sensor sensor = JsonConvert.DeserializeObject<Sensor>(responseBody)!;
    //
    //             return sensor;
    //         }
    //         catch (HttpRequestException ex)
    //         {
    //             throw new Exception("An error occurred while getting the sensor. Please check your network connection and try again.", ex);
    //         }
    //     }
    // }
}