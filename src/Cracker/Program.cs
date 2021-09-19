using System.Threading.Tasks;
using Cracker.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cracker
{
    internal static class Program
    {
        private static Task Main(string[] args) =>
            CreateHostBuilder(args).Build().RunAsync();

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
