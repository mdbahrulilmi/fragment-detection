using Avalonia.Media.Imaging;
using fragment_detection.Helpers;
using ReactiveUI;
using System;
using System.Reactive;

namespace fragment_detection.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly VideoCaptureService _videoCapture = new();
    private Bitmap? _currentFrame;

    public Bitmap? CurrentFrame
    {
        get => _currentFrame;
        set => this.RaiseAndSetIfChanged(ref _currentFrame, value);
    }

    public ReactiveCommand<Unit, Unit> StartCameraCommand { get; }
    private readonly ResultViewModel _resultViewModel;
    public ReactiveCommand<Unit, Unit> HitungFragmenCommand { get; }

    public ReactiveCommand<Unit, Unit> StopCameraCommand { get; }

    public MainViewModel()
    {
        _videoCapture.FrameReceived += bitmap =>
        {
            CurrentFrame = bitmap;
        };

        StartCameraCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            await _videoCapture.StartAsync();
        });

        StopCameraCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            await _videoCapture.StopAsync();
        });
        HitungFragmenCommand = ReactiveCommand.Create(() =>
        {
            AddTestResults();
        });
    }
    public void AddTestResults()
    {
        _resultViewModel.Results.Add(new ImageResultModel
        {
            Nama = "Test Image",
            ImagePath = "path/to/image.jpg", // Path gambar yang diupload atau dipilih
            Tanggal = DateTime.Now.ToString("yyyy-MM-dd"),
            Waktu = DateTime.Now.ToString("HH:mm:ss"),
            JumlahFragmen = 5
        });
    }
}
