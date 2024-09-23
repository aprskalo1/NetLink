using Microsoft.AspNetCore.SignalR;

namespace NetLink.API.Hubs;

public class SensorHub : Hub
{
    public async Task Subscribe(string sensorId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, sensorId);
    }

    public async Task Unsubscribe(string sensorId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, sensorId);
    }
}