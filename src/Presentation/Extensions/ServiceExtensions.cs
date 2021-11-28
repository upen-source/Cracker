using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPresentationDependencies(this IServiceCollection services)
        {
            services.AddTransient<MainWindow>();
        }

        public static void AddDataDependencies(this IServiceCollection services)
        {
            // Add data dependencies
        }
    }
}
