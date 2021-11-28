using System.Windows;

namespace Presentation.Components.Buttons
{
    public partial class GoBackButtonUserControl
    {
        public event RoutedEventHandler Click;

        public GoBackButtonUserControl()
        {
            InitializeComponent();
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, new RoutedEventArgs());
        }

        public string ButtonToolTip
        {
            get => (string)GetValue(ButtonToolTipProperty);
            set => SetValue(ButtonToolTipProperty, value);
        }

        public static readonly DependencyProperty ButtonToolTipProperty =
            DependencyProperty.Register("ButtonToolTip", typeof(string),
                typeof(GoBackButtonUserControl), new PropertyMetadata(null));
    }
}
