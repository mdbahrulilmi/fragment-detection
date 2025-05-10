using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.VisualTree;
using fragment_detection.ViewModels;

namespace fragment_detection.Views;

public partial class HistoryView : UserControl
{
    public HistoryView()
    {
        InitializeComponent();

        if (DeleteControl != null)
        {
            DeleteControl.CancelClicked += (_, __) => DeletePopUp.IsOpen = false;
            DeleteControl.DeleteClicked += (_, __) =>
            {
                // TODO: delete logic here
                DeletePopUp.IsOpen = false;
            };
        }
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
        DeletePopUp.IsOpen = true;
    }

}