using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CLInjected.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<App>();
        }

        public static void AddApplicationServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            // Inject dependencies
        }
    }
}
