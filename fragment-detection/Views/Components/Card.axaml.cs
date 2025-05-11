using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using fragment_detection.ViewModels;

namespace fragment_detection.Views
{
    public partial class Card : UserControl
    {
        public Card()
        {
            InitializeComponent();
            DataContext = new CardViewModel();
        }
    }
}
