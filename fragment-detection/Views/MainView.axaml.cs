using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using Avalonia.VisualTree;
using fragment_detection.Helpers;
using fragment_detection.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace fragment_detection.Views
{
    public partial class MainView : UserControl
    {
        private readonly VideoCaptureService _camera = new();
        private Bitmap? _latestFrame;

        public ObservableCollection<CardViewModel> Cards { get; } = new();

        public MainView()
        {
            InitializeComponent();
            DataContext = this;
            this.AttachedToVisualTree += OnAttachedToVisualTree;

            _camera.FrameReceived += bitmap =>
            {
                _latestFrame = bitmap;
                Dispatcher.UIThread.Post(() =>
                {
                    CameraFeed.Source = bitmap;
                });
            };

            _camera.CameraStarted += () =>
            {
                Dispatcher.UIThread.Post(() =>
                {
                    UploadedImagesControl.IsVisible = false;
                    UploadPlaceholder.IsVisible = false;
                    UploadMark.IsVisible = false;
                    UploadText.IsVisible = false;
                });
            };

            _camera.CameraStopped += () =>
            {
                Dispatcher.UIThread.Post(() =>
                {
                    UploadedImagesControl.IsVisible = true;
                    UploadPlaceholder.IsVisible = true;
                    UploadMark.IsVisible = true;
                    UploadText.IsVisible = true;
                });
            };
            Cards.CollectionChanged += Cards_CollectionChanged;


        }

        private void Cards_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                Dispatcher.UIThread.Post(() =>
                {
                    ScrollToRightEnd();
                }, DispatcherPriority.Background);
            }
        }

        private void ScrollToRightEnd()
        {
            if (ImagesScrollViewer != null)
            {
                ImagesScrollViewer.Offset = new Vector(ImagesScrollViewer.Extent.Width, 0);
            }
        }
        private void OnAttachedToVisualTree(object sender, VisualTreeAttachmentEventArgs e)
        {
            var window = this.GetVisualRoot() as Window;
            if (window != null)
            {
                double windowWidth = window.Bounds.Width;

                window.GetObservable(Window.BoundsProperty).Subscribe(bounds =>
                {

                });
            }
        }


        private async void TakePhoto_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (TakePhoto.Text == "Ambil Foto Langsung")
            {

                CameraFeed.IsVisible = true;
                await _camera.StartAsync();
                TakePhoto.Text = "Jepret";
                BackButton.IsVisible = true;
            }
            else if (TakePhoto.Text == "Jepret")
            {
                if (_latestFrame != null)
                {
                    var card = new CardViewModel
                    {
                        Image = _latestFrame,
                        FileName = "Foto Langsung"
                    };

                    Cards.Add(card);
                    
                    UploadedImagesControl.IsVisible = true;
                    CameraFeed.IsVisible = false;
                    FragmentButton.Background = new SolidColorBrush(Color.Parse("#3B89FF"));
                    TakePhoto.Text = "Ambil Foto Langsung";
                    Dispatcher.UIThread.Post(() =>
                    {
                        ImagesScrollViewer?.ScrollToEnd();
                    });
                }
            }
        }

        private void BackButtonClick(object? sender, PointerPressedEventArgs e)
        {
            UploadPlaceholder.IsVisible = true;
            UploadMark.IsVisible = true;
            UploadText.IsVisible = true;
            CameraFeed.IsVisible = false;
            UploadedImagesControl.IsVisible = false;
            TakePhoto.Text = "Ambil Foto Langsung";

            _camera.StopAsync();
            BackButton.IsVisible = false;
        }

        private async void FragmentButtonClick(object? sender, RoutedEventArgs e)
        {
            if (Cards.Count == 0)
                return;

            var viewModel = new ResultViewModel(Cards);
            var resultView = new ResultView
            {
                DataContext = viewModel
            };

            var mainWindow = this.FindAncestorOfType<MainWindow>();
            mainWindow?.Navigate(resultView);
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
                AllowMultiple = true,
                FileTypeFilter = new[] { ImageAll }
            });

            if (files.Count >= 1)
            {

                foreach (var file in files)
                {
                    await using var stream = await file.OpenReadAsync();
                    var bitmap = new Bitmap(stream);

                    Cards.Add(new CardViewModel
                    {
                        Image = bitmap,
                        FileName = file.Name
                    });
                }

                UploadedImagesControl.IsVisible = true;
                CameraFeed.IsVisible = false;
                await _camera.StopAsync();

                FragmentButton.Background = new SolidColorBrush(Color.Parse("#3B89FF"));
                Dispatcher.UIThread.Post(() =>
                {
                    ImagesScrollViewer?.ScrollToEnd();
                });
            }
        }

        public static FilePickerFileType ImageAll { get; } = new("All Images")
        {
            Patterns = new[] { "*.png", "*.jpg", "*.jpeg", "*.gif", "*.bmp", "*.webp" },
            AppleUniformTypeIdentifiers = new[] { "public.image" },
            MimeTypes = new[] { "image/*" }
        };
        private void DeleteImage_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is CardViewModel card)
            {
                Cards.Remove(card);
            }
        }

    }
}
