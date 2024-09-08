using NetLink.Helpers;
using NetLink.Models;
using NetLink.Session;
using NetLink.Utilities;

namespace NetLink.Services;

public interface ISensorService
{
    Task<Guid> AddSensorAsync(Sensor sensor, string? endUserId = null);
    Task<Sensor> GetSensorByNameAsync(string deviceName, string? endUserId = null);
    Task<Sensor> GetSensorByIdAsync(Guid sensorId, string? endUserId = null);
    Task<Sensor?> UpdateSensorAsync(Guid sensorId, Sensor sensor, string? endUserId = null);
    Task DeleteSensorAsync(Guid sensorId, string? endUserId = null);
}

internal class SensorService(IEndUserSessionManager endUserSessionManager, IDeveloperSessionManager developerSessionManager)
    : HttpHelper(developerSessionManager), ISensorService
{
    public async Task<Guid> AddSensorAsync(Sensor sensor, string? endUserId = null)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.AddSensorUrl, GetEffectiveUserId(endUserId))}";
        var response = await SendRequestAsync<Guid>(HttpMethod.Post, endpoint, sensor);
        return response;
    }

    public async Task<Sensor> GetSensorByNameAsync(string deviceName, string? endUserId = null)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.GetSensorByNameUrl, deviceName, GetEffectiveUserId(endUserId))}";
        var response = await SendRequestAsync<Sensor>(HttpMethod.Get, endpoint);
        return response ?? throw new Exception("Sensor not found.");
    }

    public async Task<Sensor> GetSensorByIdAsync(Guid sensorId, string? endUserId = null)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.GetSensorByIdUrl, sensorId, GetEffectiveUserId(endUserId))}";
        var response = await SendRequestAsync<Sensor>(HttpMethod.Get, endpoint);
        return response ?? throw new Exception("Sensor not found.");
    }

    public async Task<Sensor?> UpdateSensorAsync(Guid sensorId, Sensor sensor, string? endUserId = null)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.UpdateSensorUrl, sensorId, GetEffectiveUserId(endUserId))}";
        var response = await SendRequestAsync<Sensor>(HttpMethod.Put, endpoint, sensor);
        return response;
    }

    public async Task DeleteSensorAsync(Guid sensorId, string? endUserId = null)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.DeleteSensorUrl, sensorId, GetEffectiveUserId(endUserId))}";
        await SendRequestAsync(HttpMethod.Delete, endpoint);
    }

    private string GetEffectiveUserId(string? userId)
    {
        return userId ?? endUserSessionManager.GetLoggedEndUserId();
    }
}