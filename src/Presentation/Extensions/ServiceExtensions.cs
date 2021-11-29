using System.Data.Common;
using System.Data.SqlClient;
using Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Settings;
using Presentation.Windows;

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
            IConfiguration configuration = SettingsLoader.Configuration;
            services.AddSingleton(configuration);
            services.AddSingleton<DbConnection>(
                new SqlConnection(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<PeopleRepository>();
        }
    }
}
