using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;

namespace fragment_detection.Views
{
    public partial class DetailView : UserControl
    {
        public DetailView()
        {
            InitializeComponent();

            if (DeleteControl != null)
            {
                DeleteControl.CancelClicked += (_, __) => DeletePopUp.IsOpen = false;
                DeleteControl.DeleteClicked += (_, __) =>
                {
                    DeletePopUp.IsOpen = false;
                };
            }

        }
        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            DeletePopUp.IsOpen = true;
        }

        private void BackButtonClick(object sender, PointerPressedEventArgs e)
        {
            var mainWindow = this.FindAncestorOfType<MainWindow>();

            if (mainWindow != null)
            {
                mainWindow.GoBack();
            }
        }
    }
}
    