using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _services;

        public MainWindow(IServiceProvider services)
        {
            _services = services;
            InitializeComponent();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonAnotherWindow_Click(object sender, RoutedEventArgs e)
        {
            var otherWindow = _services.GetRequiredService<OtherWindow>();
            otherWindow.ShowDialog();
        }
    }
}
