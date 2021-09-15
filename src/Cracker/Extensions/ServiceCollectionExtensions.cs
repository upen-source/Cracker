using Data;
using Logic;
using Microsoft.Extensions.DependencyInjection;
using Presentation;

namespace Cracker.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataDependencies(this IServiceCollection services)
        {
            services.AddScoped<IFileUpdater, JsonFileUpdater>();
            services.AddScoped<ISomeEntityRepository, SomeJsonRepository>();
        }

        public static void AddLogicDependencies(this IServiceCollection services)
        {
            services.AddScoped<SomeService>();
        }

        public static void AddPresentationDependencies(this IServiceCollection services)
        {
            services.AddHostedService<ConsoleApp>();
        }
    }
}
