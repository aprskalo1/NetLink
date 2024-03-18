using Microsoft.Extensions.DependencyInjection;

namespace NetLink.Session
{
    public static class DeveloperSessionManagerExtensions
    {
        public static IServiceCollection AddDevTokenAuthentication(this IServiceCollection services, string devToken)
        {
            var developerSessionManager = new DeveloperSessionManager();

            developerSessionManager.AddDevTokenAuthentication(devToken);
            services.AddSingleton(developerSessionManager);

            return services;
        }
    }
}
