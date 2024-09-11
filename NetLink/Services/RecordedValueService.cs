using NetLink.Helpers;
using NetLink.Models;
using NetLink.Session;
using NetLink.Utilities;

namespace NetLink.Services;

public interface IRecordedValueService
{
    Task RecordValueBySensorNameAsync(RecordedValue recordedValue, string sensorName, string? endUserId = null);
    Task RecordValueBySensorIdAsync(RecordedValue recordedValue, Guid sensorId);

    Task<List<RecordedValue>> GetRecordedValuesAsync(Guid sensorId, bool isAscending, int? quantity = null,
        DateTime? startDate = null, DateTime? endDate = null, string? endUserId = null);
}

internal class RecordedValueService(IEndUserSessionManager endUserSessionManager, IDeveloperSessionManager developerSessionManager)
    : HttpHelper(developerSessionManager), IRecordedValueService
{
    public async Task RecordValueBySensorNameAsync(RecordedValue recordedValue, string sensorName, string? endUserId = null)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.RecordValueBySensorNameUrl, sensorName, GetEffectiveUserId(endUserId))}";
        await SendRequestAsync(HttpMethod.Post, endpoint, recordedValue);
    }

    public async Task RecordValueBySensorIdAsync(RecordedValue recordedValue, Guid sensorId)
    {
        var endpoint = $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.RecordValueBySensorIdUrl, sensorId)}";
        await SendRequestAsync(HttpMethod.Post, endpoint, recordedValue);
    }

    public async Task<List<RecordedValue>> GetRecordedValuesAsync(Guid sensorId, bool isAscending, int? quantity = null,
        DateTime? startDate = null, DateTime? endDate = null, string? endUserId = null)
    {
        var endpoint =
            $"{ApiUrls.BaseUrl}{string.Format(ApiUrls.GetRecordedValuesUrl, sensorId, GetEffectiveUserId(endUserId), quantity, isAscending, startDate, endDate)}";
        return await SendRequestAsync<List<RecordedValue>>(HttpMethod.Get, endpoint) ?? [];
    }

    private string GetEffectiveUserId(string? userId)
    {
        return userId ?? endUserSessionManager.GetLoggedEndUserId();
    }
}