using Microsoft.Extensions.DependencyInjection;

namespace NetLink.Services;

public static class SensorServicesExtensions
{
    public static IServiceCollection AddSensorServices(this IServiceCollection services)
    {
        services.AddSingleton<ISensorService, SensorService>();
        services.AddSingleton<IRecordedValueService, RecordedValueService>();
        services.AddSingleton<IEndUserManagementService, EndUserManagementService>();
        services.AddSingleton<IGroupingService, GroupingService>();

        return services;
    }
}