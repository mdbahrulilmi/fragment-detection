using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using fragment_detection.ViewModels;

namespace fragment_detection.Views
{
    public partial class DetailView : UserControl
    {
        public DetailView()
        {
            InitializeComponent();
            DataContext = this;
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
        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {

            var mainWindow = this.FindAncestorOfType<MainWindow>();

            if (mainWindow != null)
            {
                mainWindow.Navigate(new HistoryView());
            }
        }
        private void InfoButtonClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = this.FindAncestorOfType<MainWindow>();
            if (mainWindow == null) return;

            if (sender is Button btn && btn.DataContext is CardViewModel card)
            {
                var detailVM = new DetailViewModel
                {
                    SelectedCard = card,
                    Cards = CardService.Instance.Cards
                };

                var detailView = new DetailView
                {
                    DataContext = detailVM
                };

                mainWindow.Navigate(detailView);
            }
        }

    }
}
    