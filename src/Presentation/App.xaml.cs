using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Data;
using Entities;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Extensions;
using Presentation.Windows;

namespace Presentation
{
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
