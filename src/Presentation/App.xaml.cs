using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Extensions;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; set; }

        public App()
        {
            var services = new ServiceCollection();
            services.AddDataDependencies();
            services.AddPresentationDependencies();
            ServiceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ServiceProvider.GetRequiredService<MainWindow>().Show();
        }
    }
}
