using Avalonia.Controls;
using fragment_detection.Helpers;

namespace fragment_detection.Views;

public partial class MainWindow : Window
{
    private readonly NavigationService _navigationService;

    public MainWindow()
    {
        InitializeComponent();
        _navigationService = new NavigationService();

        Navigate(new MainView());
    }

    public void Navigate(UserControl control)
    {
        _navigationService.Navigate(control);
        MainContent.Content = control;
    }

    public void GoBack()
    {
        var previousPage = _navigationService.GoBack();
        if (previousPage != null)
        {
            MainContent.Content = previousPage;
        }
    }


}
