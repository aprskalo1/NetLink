using Microsoft.Extensions.DependencyInjection;

namespace NetLink.Session;

public static class SessionManagerExtensions
{
    public static IServiceCollection AuthenticateWithDevToken(this IServiceCollection services, string devToken)
    {
        services.AddSingleton<IDeveloperSessionManager, DeveloperSessionManager>(_ => new DeveloperSessionManager(devToken));
        
        return services;
    }

    public static IServiceCollection AddEndUsers(this IServiceCollection services)
    {
        services.AddSingleton<IEndUserSessionManager, EndUserSessionManager>();

        return services;
    }
}