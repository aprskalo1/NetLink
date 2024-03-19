using Microsoft.Extensions.DependencyInjection;

namespace NetLink.Session
{
    public static class SessionManagerExtensions
    {
        public static IServiceCollection AuthenticateDevToken(this IServiceCollection services, string devToken)
        {
            services.AddSingleton<IDeveloperSessionManager, DeveloperSessionManager>(provider =>
            {
                var developerSessionManager = new DeveloperSessionManager();
                developerSessionManager.AddDevTokenAuthentication(devToken);
                return developerSessionManager;
            });

            return services;
        }

        public static IServiceCollection AddEndUsers(this IServiceCollection services)
        {
            services.AddSingleton<IEndUserSessionManager, EndUserSessionManager>();

            return services;
        }
    }
}
