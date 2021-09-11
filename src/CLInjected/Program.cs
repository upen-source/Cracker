using CLInjected.Settings;
using Microsoft.Extensions.Hosting;

namespace CLInjected
{
    internal static class Program
    {
        private static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                    new Startup(Configuration.Startup).ConfigureServices(services)
                );
    }
}
