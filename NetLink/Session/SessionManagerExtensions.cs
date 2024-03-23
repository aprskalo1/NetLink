using Microsoft.Extensions.DependencyInjection;

namespace NetLink.Session
{
    public static class SessionManagerExtensions
    {
        public static IServiceCollection AuthenticateWithDevToken(this IServiceCollection services, string devToken)
        {
            var developerSessionManager = new DeveloperSessionManager();

            developerSessionManager.AddDevTokenAuthentication(devToken);
            services.AddSingleton(developerSessionManager);

            return services;
        }

        public static IServiceCollection AddEndUsers(this IServiceCollection services)
        {
            services.AddSingleton<IEndUserSessionManager, EndUserSessionManager>();

            return services;
        }
    }
}
