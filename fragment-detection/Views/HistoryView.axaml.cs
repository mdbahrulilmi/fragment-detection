using System;
using System.Collections.ObjectModel;
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

    public ObservableCollection<CardViewModel> Cards { get; }

    public HistoryView()
    {
        InitializeComponent();
        Cards = CardService.Instance.Cards;
        DataContext = this;
        Table.DeleteRequested += Table_DeleteRequested;
        if (DeleteControl != null)
        {
            DeleteControl.CancelClicked += (_, __) => DeletePopUp.IsOpen = false;
            DeleteControl.DeleteClicked += (_, __) =>
            {
                DeletePopUp.IsOpen = false;
            }; 
        }

    }

    private void BackButtonClick(object sender, PointerPressedEventArgs e)
    {
        var mainWindow = this.FindAncestorOfType<MainWindow>();

        if (mainWindow != null)
        {
            mainWindow.Navigate(new MainView());
        }
    }

    private void Table_DeleteRequested(object? sender, EventArgs e)
    {
        DeletePopUp.IsOpen = true;
    }

}