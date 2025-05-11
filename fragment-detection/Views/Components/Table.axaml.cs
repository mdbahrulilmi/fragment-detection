using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using fragment_detection.Views;
using System;

namespace fragment_detection.Views
{
    public partial class Table : UserControl
    {
        public event EventHandler DeleteRequested;
        public Table()
        {
            InitializeComponent();
        }
    
        private void BackButtonClick(object sender, PointerPressedEventArgs e)
        {
            var mainWindow = this.FindAncestorOfType<MainWindow>();

            if (mainWindow != null)
            {
                mainWindow.GoBack();
            }
        }
        private void InfoButtonClick(object sender, PointerPressedEventArgs e)
        {
            var mainWindow = this.FindAncestorOfType<MainWindow>();

            if (mainWindow != null)
            {
                mainWindow.Navigate(new DetailView());
            }
        }
        private void DeleteButtonClick(object sender, PointerPressedEventArgs e)
        {
            DeleteRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}