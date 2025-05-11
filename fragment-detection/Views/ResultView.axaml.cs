using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;

namespace fragment_detection.Views;

public partial class ResultView : UserControl
{
    private int currentIndex = 0;

    public ResultView()
    {
        InitializeComponent();
        this.AttachedToVisualTree += (_, __) => UpdateArrowVisibility();

        if (DeleteControl != null)
        {
            DeleteControl.CancelClicked += (_, __) => DeletePopUp.IsOpen = false;
            DeleteControl.DeleteClicked += (_, __) =>
            {
                DeletePopUp.IsOpen = false;
            };
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

    public void Next(object source, RoutedEventArgs args)
    {
        int totalSlides = slides.ItemCount;

        if (currentIndex < totalSlides - 1)
        {
            currentIndex++;
            slides.Next();
            UpdateArrowVisibility();
        }
    }

    public void Previous(object source, RoutedEventArgs args)
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            slides.Previous();
            UpdateArrowVisibility();
        }
    }

    private void UpdateArrowVisibility()
    {
        var leftArrow = this.FindControl<Button>("LeftArrow");
        var rightArrow = this.FindControl<Button>("RightArrow");

        int totalSlides = slides.ItemCount;

        leftArrow.IsVisible = currentIndex > 0;
        rightArrow.IsVisible = currentIndex < totalSlides - 1;
    }
}