using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using DynamicData;

namespace fragment_detection.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }
        private void Rectangle_Clicked(object sender, PointerPressedEventArgs e)
        {
            OpenFileButton_Clicked(sender, e);
        }

        private async void OpenFileButton_Clicked(object sender, RoutedEventArgs args)
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

                    UploadedImage.Source = bitmap;
                    UploadedImage.IsVisible = true;

                    UploadPlaceholder.IsVisible = false;
                    UploadMark.IsVisible = false;
                    UploadText.IsVisible = false;

                    TakePhoto.Text = "Unggah Foto";

                    //string projectDir = AppContext.BaseDirectory;
                    //string imageDir = Path.Combine(projectDir, "SavedImages");
                    //Directory.CreateDirectory(imageDir);

                    //string fileExt = Path.GetExtension(file.Name);
                    //string uniqueName = $"{Guid.NewGuid()}{fileExt}";
                    //string savePath = Path.Combine(imageDir, uniqueName);

                    //await using var outStream = new FileStream(savePath, FileMode.Create, FileAccess.Write);
                    //stream.Seek(0, SeekOrigin.Begin);
                    //await stream.CopyToAsync(outStream);

                    //Debug.WriteLine($"Image saved temporarily: {savePath}");
                }
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
