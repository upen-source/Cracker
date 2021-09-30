using System.Windows;
using MahApps.Metro.Controls;

namespace Presentation
{
    public partial class OtherWindow : MetroWindow
    {
        public OtherWindow()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

