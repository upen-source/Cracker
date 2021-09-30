using System;
using System.Windows;
using Dapplo.Microsoft.Extensions.Hosting.Wpf;
using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, IWpfShell
    {
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonAnotherWindow_Click(object sender, RoutedEventArgs e)
        {
            var otherWindow = _serviceProvider.GetRequiredService<OtherWindow>();
            otherWindow.Show();
        }
    }
}
