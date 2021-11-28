﻿using System.Windows;

namespace Presentation.Components.Buttons
{
    public partial class ButtonUserControl
    {
        public event RoutedEventHandler Click;

        public ButtonUserControl()
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

        public string ButtonContent
        {
            get => (string)GetValue(ButtonContentProperty);
            set => SetValue(ButtonContentProperty, value);
        }

        public string ButtonRadius
        {
            get => (string)GetValue(ButtonRadiusProperty);
            set => SetValue(ButtonRadiusProperty, value);
        }

        public string ButtonWidth
        {
            get => (string)GetValue(ButtonWidthProperty);
            set => SetValue(ButtonWidthProperty, value);
        }

        public string ButtonHeight
        {
            get => (string)GetValue(ButtonHeightProperty);
            set => SetValue(ButtonHeightProperty, value);
        }

        public string ButtonIsEnabled
        {
            get => (string)GetValue(ButtonIsEnabledProperty);
            set => SetValue(ButtonIsEnabledProperty, value);
        }

        public static readonly DependencyProperty ButtonToolTipProperty =
            DependencyProperty.Register("ButtonToolTip", typeof(string), typeof(ButtonUserControl),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ButtonContentProperty =
            DependencyProperty.Register("ButtonContent", typeof(string), typeof(ButtonUserControl),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ButtonRadiusProperty =
            DependencyProperty.Register("ButtonRadius", typeof(string), typeof(ButtonUserControl),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ButtonWidthProperty =
            DependencyProperty.Register("ButtonWidth", typeof(string), typeof(ButtonUserControl),
                new PropertyMetadata("120"));

        public static readonly DependencyProperty ButtonHeightProperty =
            DependencyProperty.Register("ButtonHeight", typeof(string), typeof(ButtonUserControl),
                new PropertyMetadata("35"));

        public static readonly DependencyProperty ButtonIsEnabledProperty =
            DependencyProperty.Register("ButtonIsEnabled", typeof(string),
                typeof(ButtonUserControl), new PropertyMetadata("35"));
    }
}
