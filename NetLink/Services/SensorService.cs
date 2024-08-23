using NetLink.Models;
using NetLink.Session;
using NetLink.Utilities;

namespace NetLink.Services;

public interface ISensorService
{
    public Task<Sensor> AddSensorAsync(Sensor sensor);
    //public Sensor GetSensorByName(string deviceName);
}

internal class SensorService(IDeveloperSessionManager developerSessionManager) : ISensorService
{
    public async Task<Sensor> AddSensorAsync(Sensor sensor)
    {
        const string endUserId = "a3af25fb-ac68-4de7-b2c8-431c1fcae994";
        var addSensorEndpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.AddSensorUrl, endUserId)}";

        var httpClient = await developerSessionManager.GetAuthenticatedHttpClientAsync();
        //logic to add sensor
        
        return new Sensor("asdasd");
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