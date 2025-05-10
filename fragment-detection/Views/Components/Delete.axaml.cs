using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace fragment_detection.Views
{
    public partial class Delete : UserControl
    {
        public event EventHandler? CancelClicked;
        public event EventHandler? DeleteClicked;

        public Delete()
        {
            InitializeComponent();
        }

        private void OnCancelClick(object? sender, RoutedEventArgs e)
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
        }

        private void OnDeleteClick(object? sender, RoutedEventArgs e)
        {
            DeleteClicked?.Invoke(this, EventArgs.Empty);
        }
    }

}
