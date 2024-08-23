using Microsoft.Extensions.DependencyInjection;

namespace NetLink.Services;

public static class ServicesExtensions
{
    public static IServiceCollection AddSensorServices(this IServiceCollection services)
    {
        services.AddSingleton<ISensorService, SensorService>();
        services.AddSingleton<IRecordedValueService, RecordedValueService>();

        return services;
    }
}