using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using Avalonia.VisualTree;
using fragment_detection.Helpers;
using System;
using System.Threading.Tasks;

namespace fragment_detection.Views
{
    public partial class MainView : UserControl
    {
        private readonly VideoCaptureService _camera = new();
        private Bitmap? _latestFrame;

        public MainView()
        {
            InitializeComponent();

            _camera.FrameReceived += bitmap =>
            {
                _latestFrame = bitmap;
                Dispatcher.UIThread.Post(() =>
                {
                    CameraFeed.Source = bitmap;
                });
            };
        }

        private async void TakePhoto_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (TakePhoto.Text == "Ambil Foto Langsung")
            {
                UploadedImage.IsVisible = false;
                UploadMark.IsVisible = false;
                UploadText.IsVisible = false;
                UploadPlaceholder.IsVisible = false;

                CameraFeed.IsVisible = true;
                await _camera.StartAsync();
                TakePhoto.Text = "Jepret";
            }
            else if (TakePhoto.Text == "Jepret")
            {
                if (_latestFrame != null)
                {
                    UploadedImage.Source = _latestFrame;
                    UploadedImage.IsVisible = true;
                    CameraFeed.IsVisible = false;
                    FragmentButton.Background = new SolidColorBrush(Color.Parse("#3B89FF"));
                    TakePhoto.Text = "Ambil Foto Langsung";
                }
            }
        }


        private async void FragmentButtonClick(object? sender, RoutedEventArgs e)
        {
            if (!UploadedImage.IsVisible)
            {
                return;
            }

            var mainWindow = this.FindAncestorOfType<MainWindow>();

            if (mainWindow != null)
            {
                mainWindow.Navigate(new ResultView());
            }
        }

        private void Rectangle_Clicked(object sender, PointerPressedEventArgs e)
        {
            OpenFileButton_Clicked(sender, e);
        }

        private async void OpenFileButton_Clicked(object? sender, RoutedEventArgs args)
        {
            var topLevel = TopLevel.GetTopLevel(this);

            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Open Image File",
                AllowMultiple = false,
                FileTypeFilter = new[] { ImageAll }
            });

            if (files.Count >= 1)
            {
                var file = files[0];
                await using var stream = await file.OpenReadAsync();
                var bitmap = new Bitmap(stream);

                UploadedImage.Source = bitmap;
                UploadedImage.IsVisible = true;

                CameraFeed.IsVisible = false;
                await _camera.StopAsync();

                UploadPlaceholder.IsVisible = false;
                UploadMark.IsVisible = false;
                UploadText.IsVisible = false;

                FragmentButton.Background = new SolidColorBrush(Color.Parse("#3B89FF"));
            }
        }

        public static FilePickerFileType ImageAll { get; } = new("All Images")
        {
            Patterns = new[] { "*.png", "*.jpg", "*.jpeg", "*.gif", "*.bmp", "*.webp" },
            AppleUniformTypeIdentifiers = new[] { "public.image" },
            MimeTypes = new[] { "image/*" }
        };
    }
}
