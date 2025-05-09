using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace fragment_detection.Views;

public partial class HeaderView : UserControl
{
    public HeaderView()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void HistoryButtonClick(object sender, RoutedEventArgs e)
    {
    
    }
}