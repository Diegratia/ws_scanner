using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using ws_scanner.Application.Dtos;
using ws_scanner.Application.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using static System.Net.Mime.MediaTypeNames;

namespace ws_scanner.Application.Services
{
    public class ImagePipelineService : IImagePipelineService
    {
        private readonly IImageWatcher _watcher;
        private readonly IOcrApiClient _ocr;
        private readonly IWebSocketService _ws;

        private WsRequest? _wsRequest;
        private string? _pendingImage;

        private readonly object _processLock = new();
        private bool _isProcessing;

        public ImagePipelineService(
            IImageWatcher watcher,
            IOcrApiClient ocr,
            IWebSocketService ws)
        {
            _watcher = watcher;
            _ocr = ocr;
            _ws = ws;

            _watcher.OnImageReady += OnImageArrived;
            _ws.OnRequestReceived += OnWsRequest;
        }

        public void Start() => _watcher.Start();
        public void Stop() => _watcher.Stop();

        // 📁 FILE MASUK
        private void OnImageArrived(string imagePath)
        {
            Debug.WriteLine($"📁 IMAGE ARRIVED: {imagePath}");
            _pendingImage = imagePath;
            _ = TryProcessAsync();
        }

        // 📩 WS REQUEST MASUK
        private Task OnWsRequest(WsRequest req)
        {
            lock (_processLock)
            {
                _wsRequest = req;
            }

            Debug.WriteLine(
                $"📩 WS CONTEXT SET: doc={req.DocType}, action={req.ActionType}, source={req.ActionSource}"
            );

            return TryProcessAsync();
        }

        private static string ImageToBase64(string imagePath)
        {
            var bytes = File.ReadAllBytes(imagePath);
            return Convert.ToBase64String(bytes);
        }



        private static string ImageToBase64DataUri(string imagePath)
        {
            var bytes = File.ReadAllBytes(imagePath);
            var base64 = Convert.ToBase64String(bytes);

            var ext = Path.GetExtension(imagePath).ToLowerInvariant();
            var mime = ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };

            return $"data:{mime};base64,{base64}";
        }


        // 🚀 PROSES UTAMA
        private async Task TryProcessAsync()
        {
            string? imagePath;
            WsRequest? req;

            lock (_processLock)
            {
                if (_isProcessing || _pendingImage == null || _wsRequest == null)
                {
                    Debug.WriteLine("⏳ WAITING (image or ws context missing or already processing)");
                    return;
                }

                _isProcessing = true;
                imagePath = _pendingImage;
                req = _wsRequest;

                _pendingImage = null;
                _wsRequest = null;
            }

            try
            {
                Debug.WriteLine($"🚀 OCR START ({req!.DocType})");

                await _ws.SendAsync(
                   JsonSerializer.Serialize(
                       WsResponse.Ready(
                           req,
                           new { hasImage = true }
                       )
                   )
               );

                var ocrRequest = new OcrRequest(
                    imagePath!,
                    req.DocType
                );

                await WaitUntilFileReadyAsync(imagePath);
                await ResizeImageIfNeededAsync(imagePath);
                var raw = await _ocr.SendAsync(ocrRequest);
                var parsed = JsonSerializer.Deserialize<object>(raw);

                await WaitUntilFileReadyAsync(imagePath);
                var base64 = ImageToBase64DataUri(imagePath!);

                // ✅ READY + BASE64
                //await _ws.SendAsync(
                //    JsonSerializer.Serialize(
                //        WsResponse.Ready(
                //            req,
                //            new { imageBase64 = base64 }
                //        )
                //    )
                //);
                await _ws.SendAsync(
                base64
                );

                // ✅ OCR RESULT
                await _ws.SendAsync(
                    JsonSerializer.Serialize(
                        WsResponse.OcrResult(
                            req,
                            parsed!
                        )
                    )
                );

                Debug.WriteLine("✅ OCR DONE, WS RESPONSE SENT");
            }
            catch (Exception ex)
            {
                await _ws.SendAsync(
                    JsonSerializer.Serialize(
                        WsResponse.Error(_wsRequest, ex.Message)
                    )
                );
            }
            finally
            {
                lock (_processLock)
                {
                    _isProcessing = false;
                }
            }
        }

        private static async Task WaitUntilFileReadyAsync(string path)
        {
            const int maxRetry = 20;

            for (int i = 0; i < maxRetry; i++)
            {
                try
                {
                    using var fs = File.Open(
                        path,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.None //HARUS EXCLUSIVE
                    );

                    return; // file siap
                }
                catch (IOException)
                {
                    await Task.Delay(300);
                }
            }

            throw new IOException($"File still locked: {path}");
        }

        private static async Task ResizeImageIfNeededAsync(
        string imagePath,
        int maxWidth = 1400,
        int jpegQuality = 60
)
        {
            using var image = await SixLabors.ImageSharp.Image.LoadAsync(imagePath);

            if (image.Width <= maxWidth)
                return;

            var ratio = (double)maxWidth / image.Width;
            var newHeight = (int)(image.Height * ratio);

            image.Mutate(ctx =>
                ctx.Resize(new ResizeOptions
                {
                    Mode = ResizeMode.Max,
                    Size = new Size(maxWidth, newHeight),
                    Sampler = KnownResamplers.Lanczos3
                })
            );

            var ext = Path.GetExtension(imagePath).ToLowerInvariant();

            if (ext is ".jpg" or ".jpeg")
            {
                await image.SaveAsync(
                    imagePath,
                    new JpegEncoder
                    {
                        Quality = jpegQuality
                    }
                );
            }
            else
            {
                // PNG / lainnya
                await image.SaveAsync(imagePath);
            }
        }


    }
}
