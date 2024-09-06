using NetLink.Helpers;
using NetLink.Models;
using NetLink.Session;
using NetLink.Utilities;

namespace NetLink.Services;

public interface ISensorService
{
    public Task<Guid> AddSensorAsync(Sensor sensor);
    public Task<Sensor> GetSensorByNameAsync(string deviceName);
    public Task<Sensor> GetSensorByIdAsync(Guid sensorId);
    public Task<Sensor?> UpdateSensorAsync(Guid sensorId, Sensor sensor);
    public Task DeleteSensorAsync(Guid sensorId);
}

internal class SensorService(
    IEndUserSessionManager endUserSessionManager,
    IDeveloperSessionManager developerSessionManager
) : HttpHelper(developerSessionManager), ISensorService
{
    public async Task<Guid> AddSensorAsync(Sensor sensor)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.AddSensorUrl, endUserSessionManager.GetLoggedEndUserId())}";
        var response = await SendRequestAsync<Guid>(HttpMethod.Post, endpoint, sensor);
        return response;
    }

    public async Task<Sensor> GetSensorByNameAsync(string deviceName)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.GetSensorByNameUrl, deviceName, endUserSessionManager.GetLoggedEndUserId())}";
        var response = await SendRequestAsync<Sensor>(HttpMethod.Get, endpoint);
        return response ?? throw new Exception("Sensor not found.");
    }

    public async Task<Sensor> GetSensorByIdAsync(Guid sensorId)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.GetSensorByIdUrl, sensorId, endUserSessionManager.GetLoggedEndUserId())}";
        var response = await SendRequestAsync<Sensor>(HttpMethod.Get, endpoint);
        return response ?? throw new Exception("Sensor not found.");
    }

    public async Task<Sensor?> UpdateSensorAsync(Guid sensorId, Sensor sensor)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.UpdateSensorUrl, sensorId, endUserSessionManager.GetLoggedEndUserId())}";
        var response = await SendRequestAsync<Sensor>(HttpMethod.Put, endpoint, sensor);
        return response;
    }

    public async Task DeleteSensorAsync(Guid sensorId)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.DeleteSensorUrl, sensorId, endUserSessionManager.GetLoggedEndUserId())}";
        await SendRequestAsync(HttpMethod.Delete, endpoint);
    }
}