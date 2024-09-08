using NetLink.Helpers;
using NetLink.Models;
using NetLink.Session;
using NetLink.Utilities;

namespace NetLink.Services;

internal interface IRecordedValueService
{
    Task RecordValueBySensorNameAsync(RecordedValue recordedValue, string sensorName, string endUserId);
    Task RecordValueBySensorIdAsync(RecordedValue recordedValue, Guid sensorId);
    Task<List<RecordedValue>> GetRecordedValuesAsync(Guid sensorId, string endUserId, int quantity, bool isAscending);
}

internal class RecordedValueService(IEndUserSessionManager endUserSessionManager, IDeveloperSessionManager developerSessionManager)
    : HttpHelper(developerSessionManager), IRecordedValueService
{
    public async Task RecordValueBySensorNameAsync(RecordedValue recordedValue, string sensorName, string endUserId)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.RecordValueBySensorNameUrl, sensorName, GetEffectiveUserId(endUserId))}";
        await SendRequestAsync(HttpMethod.Post, endpoint, recordedValue);
    }

    public async Task RecordValueBySensorIdAsync(RecordedValue recordedValue, Guid sensorId)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.RecordValueBySensorIdUrl, sensorId)}";
        await SendRequestAsync(HttpMethod.Post, endpoint, recordedValue);
    }

    public async Task<List<RecordedValue>> GetRecordedValuesAsync(Guid sensorId, string endUserId, int quantity, bool isAscending)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.GetRecordedValuesUrl, sensorId, GetEffectiveUserId(endUserId), quantity, isAscending)}";
        return await SendRequestAsync<List<RecordedValue>>(HttpMethod.Get, endpoint) ?? [];
    }

    private string GetEffectiveUserId(string? userId)
    {
        return userId ?? endUserSessionManager.GetLoggedEndUserId();
    }
}