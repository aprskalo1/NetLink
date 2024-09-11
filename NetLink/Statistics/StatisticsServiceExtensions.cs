using Microsoft.Extensions.DependencyInjection;

namespace NetLink.Statistics;

public static class StatisticsServiceExtensions
{
    public static IServiceCollection AddStatisticsServices(this IServiceCollection services)
    {
        services.AddSingleton<IStatisticsService, StatisticsService>();

        return services;
    }
}