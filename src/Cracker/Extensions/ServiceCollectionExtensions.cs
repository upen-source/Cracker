using System.Globalization;
using Data;
using Logic;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Presentation;

namespace Cracker.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataDependencies(this IServiceCollection services)
        {
            services.AddSingleton(_ => new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Culture          = CultureInfo.InvariantCulture,
                Formatting       = Formatting.Indented
            });
            services.AddScoped<IFileUpdater, JsonFileChannel>();
            services.AddScoped<IFileContentMapper, JsonFileChannel>();
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