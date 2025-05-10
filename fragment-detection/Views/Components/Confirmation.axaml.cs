using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace fragment_detection.Views;

public partial class Confirmation : UserControl
{
    public event EventHandler? CancelClicked;
    public event EventHandler? SaveClicked;
    public Confirmation()
    {
        InitializeComponent();
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        CancelClicked?.Invoke(this, EventArgs.Empty);
    }

    private void OnSaveClick(object? sender, RoutedEventArgs e)
    {
        SaveClicked?.Invoke(this, EventArgs.Empty);
    }

}