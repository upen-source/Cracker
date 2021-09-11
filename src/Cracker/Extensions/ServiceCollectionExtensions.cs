using Cracker.Presentation;
using Microsoft.Extensions.DependencyInjection;

namespace Cracker.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPresentationDependencies(this IServiceCollection services)
        {
            services.AddHostedService<ConsoleApp>();
        }

        public static void AddDataDependencies(this IServiceCollection services)
        {
            // Inject dependencies
        }

        public static void AddLogicDependencies(this IServiceCollection services)
        {
            // Inject dependencies
        }

        public static void AddEntityDependencies(this IServiceCollection services)
        {
            // Inject dependencies
        }

    }
}
