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
    }
}
