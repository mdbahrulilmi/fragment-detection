using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;

namespace fragment_detection.Views;

public partial class ResultView : UserControl
{
    public ResultView()
    {
        AvaloniaXamlLoader.Load(this);
    }
    private void SaveButtonClick(object sender, RoutedEventArgs e)
    {

        var historyView = new HistoryView();

        if (this.Parent is Avalonia.Controls.Window window)
        {
            window.Content = historyView;
        }
        else
        {
            var mainWindow = this.FindAncestorOfType<Avalonia.Controls.Window>();
            if (mainWindow != null)
            {
                mainWindow.Content = historyView;
            }
        }
    }
    private void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
    }
}