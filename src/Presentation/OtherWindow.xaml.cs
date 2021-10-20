using System.Windows;

namespace Presentation
{
    public partial class OtherWindow : Window
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

