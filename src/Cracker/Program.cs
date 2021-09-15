using Cracker.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cracker
{
    internal static class Program
    {
        private static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(InjectDependencies);

        private static void InjectDependencies(IServiceCollection services)
        {
            services.AddDataDependencies();
            services.AddLogicDependencies();
            services.AddPresentationDependencies();
        }
    }
}
