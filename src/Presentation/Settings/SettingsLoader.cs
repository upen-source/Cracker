using Microsoft.Extensions.Configuration;

namespace Presentation.Settings
{
    public class SettingsLoader
    {
        public static IConfiguration Configuration =>
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();
    }
}
