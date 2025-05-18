using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using fragment_detection.ViewModels;
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
            if (mainWindow == null) return;

            if (sender is Image img && img.DataContext is CardViewModel card)
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


        private void DeleteButtonClick(object sender, PointerPressedEventArgs e)
        {
            DeleteRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}