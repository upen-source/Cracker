using System.Threading.Tasks;
using Cracker.Extensions;
using Dapplo.Microsoft.Extensions.Hosting.AppServices;
using Dapplo.Microsoft.Extensions.Hosting.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Presentation;

namespace Cracker
{
    internal static class Program
    {
        private static Task Main(string[] args) =>
            CreateHostBuilder(args).Build().RunAsync();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWpfApp()
                .ConfigureServices(InjectDependencies);

        private static IHostBuilder ConfigureWpfApp(this IHostBuilder hostBuilder)
        {
            return hostBuilder
                .ConfigureWpf(wpfBuilder =>
                {
                    wpfBuilder.UseApplication<App>();
                    wpfBuilder.UseWindow<MainWindow>();
                })
                .ConfigureSingleInstance()
                .UseWpfLifetime();
        }

        private static IHostBuilder ConfigureSingleInstance(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureSingleInstance(builder =>
            {
                builder.MutexId = "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}";
                builder.WhenNotFirstInstance = (hostingEnvironment, logger) =>
                {
                    logger.LogWarning(
                        $"Application {hostingEnvironment.ApplicationName} already running");
                };
            });
        }

        private static void InjectDependencies(IServiceCollection services)
        {
            services.AddDataDependencies();
            services.AddLogicDependencies();
            services.AddPresentationDependencies();
        }
    }
}
