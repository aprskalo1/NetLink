using System.Text;
using MQTTnet.Server;
using NetLink.API.DTOs.Request;
using NetLink.API.Exceptions;

namespace NetLink.API.Services.MQTT;

public interface IMqttService
{
    Task OnClientConnected(ClientConnectedEventArgs eventArgs);
    Task ValidateConnection(ValidatingConnectionEventArgs eventArgs);
    Task OnMessageReceived(InterceptingPublishEventArgs eventArgs);
}

public class MqttService(IServiceProvider serviceProvider) : IMqttService
{
    public Task OnClientConnected(ClientConnectedEventArgs eventArgs)
    {
        Console.WriteLine($"Client '{eventArgs.ClientId}' connected."); //TODO: Introduce logging for the future
        return Task.CompletedTask;
    }

    public Task ValidateConnection(ValidatingConnectionEventArgs eventArgs)
    {
        Console.WriteLine($"Client '{eventArgs.ClientId}' wants to connect. Accepting!");
        eventArgs.ReasonCode = MQTTnet.Protocol.MqttConnectReasonCode.Success;
        return Task.CompletedTask;
    }

    public async Task OnMessageReceived(InterceptingPublishEventArgs eventArgs)
    {
        var payload = eventArgs.ApplicationMessage.PayloadSegment.ToArray();
        var payloadString = Encoding.UTF8.GetString(payload);

        var recordedValueRequest = System.Text.Json.JsonSerializer.Deserialize<RecordedValueRequestDto>(payloadString);

        if (recordedValueRequest == null)
        {
            throw new RecordedValueException("Invalid payload received.");
        }

        var topicParts = eventArgs.ApplicationMessage.Topic.Split('/');
        var sensorId = topicParts[2];

        using (var scope = serviceProvider.CreateScope())
        {
            var sensorOperationsService = scope.ServiceProvider.GetRequiredService<ISensorOperationsService>();
            await sensorOperationsService.RecordValueRemotelyAsync(recordedValueRequest, Guid.Parse(sensorId));
        }

        Console.WriteLine($"Recorded value received from Sensor ID: {sensorId}");
    }
}