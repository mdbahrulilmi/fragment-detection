using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;

namespace fragment_detection.Views;

public partial class HeaderView : UserControl
{
    public HeaderView()
    {
        InitializeComponent();
    }

    private void HistoryButtonClick(object sender, PointerPressedEventArgs e)
    {
        var mainWindow = this.FindAncestorOfType<MainWindow>();

        if (mainWindow != null)
        {
            mainWindow.Navigate(new HistoryView());
        }
    }
}