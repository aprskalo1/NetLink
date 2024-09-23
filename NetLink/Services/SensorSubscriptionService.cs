using Microsoft.AspNetCore.SignalR.Client;
using NetLink.Models;
using NetLink.Utilities;

namespace NetLink.Services;

public interface ISensorSubscriptionService
{
    event Action<RecordedValue>? OnRecordedValueReceived;
    Task StartListeningAsync(Guid sensorId);
    Task StopListeningAsync(Guid sensorId);
}

internal class SensorSubscriptionService : ISensorSubscriptionService
{
    private readonly HubConnection _hubConnection;

    public SensorSubscriptionService()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(ApiUrls.SignalRUrl)
            .Build();

        _hubConnection.On<RecordedValue>("ReceiveRecordedValue",
            (recordedValue) => { OnRecordedValueReceived?.Invoke(recordedValue); });
    }

    public event Action<RecordedValue>? OnRecordedValueReceived;

    public async Task StartListeningAsync(Guid sensorId)
    {
        if (_hubConnection.State == HubConnectionState.Disconnected)
        {
            await _hubConnection.StartAsync();
        }

        await _hubConnection.SendAsync("Subscribe", sensorId);
    }

    public async Task StopListeningAsync(Guid sensorId)
    {
        await _hubConnection.StopAsync();
        await _hubConnection.SendAsync("Unsubscribe", sensorId);
    }
}