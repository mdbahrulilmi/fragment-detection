using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using FlashCap;

namespace fragment_detection.Helpers
{
    public class VideoCaptureService
    {
        private CaptureDevice? _device;

        public event Action<Bitmap>? FrameReceived;

        public async Task StartAsync()
        {
            var devices = new CaptureDevices();
            var descriptor = devices.EnumerateDescriptors().ElementAtOrDefault(0);
            if (descriptor == null)
            {
                Console.WriteLine("No camera found");
                return;
            }

            _device = await descriptor.OpenAsync(
                descriptor.Characteristics[0],
                async bufferScope =>
                {
                    try
                    {
                        byte[] image = bufferScope.Buffer.ExtractImage();
                        using var ms = new MemoryStream(image);
                        var bitmap = new Bitmap(ms);
                        FrameReceived?.Invoke(bitmap);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Frame error: {ex.Message}");
                    }
                });

            await _device.StartAsync(); 
        }

        public async Task StopAsync()
        {
            if (_device is not null)
            {
                await _device.StopAsync(); 
                _device.Dispose();         
                _device = null;
            }
        }
    }
}
