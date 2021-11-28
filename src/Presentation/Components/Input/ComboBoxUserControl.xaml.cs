﻿using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Presentation.Components.Input
{
    public partial class ComboBoxUserControl
    {
        public Func<Task> OnSelectionChangedAction { get; set; }
        public Func<Task> OnDropDownClosedAction   { get; set; }

        public ComboBoxUserControl()
        {
            InitializeComponent();
            ItemSourceBinding();
        }

        private void ComboBoxInput_OnSelectionChanged(object sender,
            SelectionChangedEventArgs e)
        {
            OnSelectionChangedAction?.Invoke();
        }

        private void ComboBoxInput_OnDropDownClosed(object sender, EventArgs e)
        {
            OnDropDownClosedAction?.Invoke();
        }

        private void ItemSourceBinding()
        {
            Binding binding = new("ComboBoxItemsSource")
            {
                Source = this
            };
            _ = ComboBoxInput.SetBinding(ItemsControl.ItemsSourceProperty, binding);
        }

        public string FieldText
        {
            get => (string)GetValue(FieldTextHintProperty);
            set => SetValue(FieldTextHintProperty, value);
        }

        public string ComboBoxHint
        {
            get => (string)GetValue(ComboBoxHintProperty);
            set => SetValue(ComboBoxHintProperty, value);
        }

        public string ComboBoxWidth
        {
            get => (string)GetValue(ComboBoxWidthProperty);
            set => SetValue(ComboBoxWidthProperty, value);
        }

        public string ComboBoxFontSize
        {
            get => (string)GetValue(ComboBoxFontSizeProperty);
            set => SetValue(ComboBoxFontSizeProperty, value);
        }

        public IEnumerable ComboBoxItemsSource
        {
            get => (IEnumerable)GetValue(ComboBoxItemsSourceProperty);
            set => SetValue(ComboBoxItemsSourceProperty, value);
        }

        public string ComboBoxHeight
        {
            get => (string)GetValue(ComboBoxHeightProperty);
            set => SetValue(ComboBoxHeightProperty, value);
        }

        public object ComboBoxSelectedItem
        {
            get => GetValue(ComboBoxSelectedItemProperty);
            set => SetValue(ComboBoxSelectedItemProperty, value);
        }

        public string ComboBoxSelectedIndex
        {
            get => (string)GetValue(ComboBoxSelectedIndexProperty);
            set => SetValue(ComboBoxSelectedIndexProperty, value);
        }

        public static readonly DependencyProperty FieldTextHintProperty =
            DependencyProperty.Register("FieldText", typeof(string), typeof(ComboBoxUserControl),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ComboBoxHintProperty =
            DependencyProperty.Register("ComboBoxHint", typeof(string),
                typeof(ComboBoxUserControl), new PropertyMetadata(null));

        public static readonly DependencyProperty ComboBoxWidthProperty =
            DependencyProperty.Register("ComboBoxWidth", typeof(string),
                typeof(ComboBoxUserControl), new PropertyMetadata("200"));

        public static readonly DependencyProperty ComboBoxHeightProperty =
            DependencyProperty.Register("ComboBoxHeight", typeof(string),
                typeof(ComboBoxUserControl), new PropertyMetadata("46"));

        public static readonly DependencyProperty ComboBoxFontSizeProperty =
            DependencyProperty.Register("ComboBoxFontSize", typeof(string),
                typeof(ComboBoxUserControl), new PropertyMetadata("15"));

        public static readonly DependencyProperty ComboBoxItemsSourceProperty =
            DependencyProperty.Register("ComboBoxItemsSource", typeof(IEnumerable),
                typeof(ComboBoxUserControl), new PropertyMetadata(null));

        public static readonly DependencyProperty ComboBoxSelectedItemProperty =
            DependencyProperty.Register("ComboBoxSelectedItem", typeof(object),
                typeof(ComboBoxUserControl), new PropertyMetadata(null));

        public static readonly DependencyProperty ComboBoxSelectedIndexProperty =
            DependencyProperty.Register("ComboBoxSelectedIndex", typeof(string),
                typeof(ComboBoxUserControl), new PropertyMetadata("15"));
    }
}
