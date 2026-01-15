//using System;
//using System.Diagnostics;
//using System.Text.Json;
//using System.Threading.Tasks;
//using ws_scanner.Application.Interfaces;

//namespace ws_scanner.Application.Services
//{
//    public class ImagePipelineService
//    {
//        private readonly IImageWatcher _watcher;
//        private readonly IOcrApiClient _ocr;
//        private readonly IWebSocketService _ws;

//        private string? _pendingImage;   // file yang masuk duluan
//        private string? _docType;        // type dari WS

//        public ImagePipelineService(
//            IImageWatcher watcher,
//            IOcrApiClient ocr,
//            IWebSocketService ws)
//        {
//            _watcher = watcher;
//            _ocr = ocr;
//            _ws = ws;

//            _watcher.OnImageReady += OnImageArrived;
//            _ws.OnDocumentTypeReceived += OnDocTypeReceived;
//        }

//        public void Start() => _watcher.Start();
//        public void Stop() => _watcher.Stop();

//        // 1️⃣ FILE MASUK
//        private void OnImageArrived(string imagePath)
//        {
//            Debug.WriteLine($"📁 IMAGE ARRIVED: {imagePath}");
//            _pendingImage = imagePath;

//            TryProcess();
//        }

//        // 2️⃣ WS MESSAGE MASUK
//        private async Task OnDocTypeReceived(string type)
//        {
//            Debug.WriteLine($"📩 DOC TYPE RECEIVED: {type}");
//            _docType = type;

//            TryProcess();
//            await Task.CompletedTask;
//        }

//        private static string ImageToBase64(string imagePath)
//        {
//            var bytes = File.ReadAllBytes(imagePath);
//            return Convert.ToBase64String(bytes);
//        }

//        // 3️⃣ JALANKAN OCR KALAU SIAP
//        //private async void TryProcess()
//        //{
//        //    if (_pendingImage == null || _docType == null)
//        //    {
//        //        Debug.WriteLine("⏳ WAITING (image or docType missing)");
//        //        return;
//        //    }

//        //    var image = _pendingImage;
//        //    var type = _docType;

//        //    // reset dulu biar 1x proses
//        //    _pendingImage = null;
//        //    _docType = null;

//        //    try
//        //    {
//        //        Debug.WriteLine($"🚀 OCR START ({type})");
//        //        var result = await _ocr.SendAsync(image, type);
//        //        Debug.WriteLine("✅ OCR DONE");

//        //        await _ws.SendAsync(result);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        await _ws.SendAsync(
//        //            $"{{\"error\":true,\"message\":\"{ex.Message}\"}}");
//        //    }
//        //}

//        private async void TryProcess()
//        {
//            if (_pendingImage == null || _docType == null)
//            {
//                Debug.WriteLine("⏳ WAITING (image or docType missing)");
//                return;
//            }

//            var imagePath = _pendingImage;
//            var type = _docType;

//            // reset agar hanya diproses 1x
//            _pendingImage = null;
//            _docType = null;

//            try
//            {
//                Debug.WriteLine($"🚀 OCR START ({type})");

//                var ocrResult = await _ocr.SendAsync(imagePath, type);
//                var base64 = ImageToBase64(imagePath);

//                var payloadBase64 = new WsOcrPayloadBase64(
//                    Type: type,
//                    ImageBase64: base64,
//                    Timestamp: DateTime.UtcNow
//                );

//                var payloadJson = new WsOcrPayloadOcr(
//                Type: type,
//                ApiResponse: ocrResult,
//                Timestamp: DateTime.UtcNow
//            );

//                var jsonBase64 = JsonSerializer.Serialize(payloadBase64);
//                var jsonOcr = JsonSerializer.Serialize(payloadJson);


//                Debug.WriteLine("✅ OCR DONE, SEND WS JSON");
//                await _ws.SendAsync(jsonBase64);
//                await _ws.SendAsync(jsonOcr);

//            }
//            catch (Exception ex)
//            {
//                var errorJson = JsonSerializer.Serialize(new
//                {
//                    error = true,
//                    message = ex.Message
//                });

//                await _ws.SendAsync(errorJson);
//            }
//        }


//    private record WsOcrPayloadBase64(
//    string Type,
//    string ImageBase64,
//    DateTime Timestamp
//);

//        private record WsOcrPayloadOcr(
//string Type,
//string ApiResponse,
//DateTime Timestamp
//);



//    }
//}




using System.Diagnostics;
using System.Text.Json;
using ws_scanner.Application.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using ws_scanner.Application.Interfaces;
using ws_scanner.Application.Dtos;

namespace ws_scanner.Application.Services
{
    public class ImagePipelineService
    {
        private readonly IImageWatcher _watcher;
        private readonly IOcrApiClient _ocr;
        private readonly IWebSocketService _ws;

        private string? _pendingImage;   // file yang masuk duluan
        private string? _docType;        // type dari WS

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
            _ws.OnDocumentTypeReceived += OnDocTypeReceived;
        }

        public void Start() => _watcher.Start();
        public void Stop() => _watcher.Stop();

        // 1️⃣ FILE MASUK
        private void OnImageArrived(string imagePath)
        {
            Debug.WriteLine($"📁 IMAGE ARRIVED: {imagePath}");
            _pendingImage = imagePath;

            // fire-and-forget dari event sinkron
            _ = TryProcessAsync();
        }

        // 2️⃣ WS MESSAGE MASUK
        private async Task OnDocTypeReceived(string type)
        {
            Debug.WriteLine($"📩 DOC TYPE RECEIVED: {type}");
            _docType = type;

            await TryProcessAsync();
        }

        private static string ImageToBase64(string imagePath)
        {
            var bytes = File.ReadAllBytes(imagePath);
            return Convert.ToBase64String(bytes);
        }

        // 3️⃣ JALANKAN OCR KALAU SIAP (async Task, bukan async void)
        private async Task TryProcessAsync()
        {
            string? imagePath;
            string? type;

            lock (_processLock)
            {
                if (_isProcessing || _pendingImage == null || _docType == null)
                {
                    Debug.WriteLine("⏳ WAITING (image or docType missing or already processing)");
                    return;
                }

                // ambil snapshot lalu reset agar hanya diproses 1x
                _isProcessing = true;
                imagePath = _pendingImage;
                type = _docType;
                _pendingImage = null;
                _docType = null;
            }

            try
            {
                Debug.WriteLine($"🚀 OCR START ({type})");

                var ocrResult = await _ocr.SendAsync(imagePath!, type!);
                var base64 = ImageToBase64(imagePath!);

                // 1️⃣ READY + BASE64
                var readyMsg = WsResponse.Ready(
                    type!,
                    new { imageBase64 = base64 }
                );
                await _ws.SendAsync(JsonSerializer.Serialize(readyMsg));

                // 2️⃣ OCR RESULT
                var ocrMsg = WsResponse.OcrResult(
                   type!,
                   ocrResult!
               );

                await _ws.SendAsync(JsonSerializer.Serialize(ocrMsg));

                Debug.WriteLine("✅ OCR DONE, WS RESPONSE SENT");

                //var payloadBase64 = new WsOcrPayloadBase64(
                //    Type: type!,
                //    ImageBase64: base64,
                //    Timestamp: DateTime.UtcNow
                //);

                //var payloadJson = new WsOcrPayloadOcr(
                //    Type: type!,
                //    ApiResponse: ocrResult,
                //    Timestamp: DateTime.UtcNow
                //);

                //var jsonBase64 = JsonSerializer.Serialize(payloadBase64);
                //var jsonOcr = JsonSerializer.Serialize(payloadJson);

                //Debug.WriteLine("✅ OCR DONE, SEND WS JSON");
                //await _ws.SendAsync(jsonBase64);
                //await _ws.SendAsync(jsonOcr);
            }
            catch (Exception ex)
            {
                var errorJson = JsonSerializer.Serialize(new
                {
                    error = true,
                    message = ex.Message
                });

                await _ws.SendAsync(errorJson);
            }
            finally
            {
                lock (_processLock)
                {
                    _isProcessing = false;
                }
            }
        }

        private record WsOcrPayloadBase64(
            string Type,
            string ImageBase64,
            DateTime Timestamp
        );

        private record WsOcrPayloadOcr(
            string Type,
            string ApiResponse,
            DateTime Timestamp
        );
    }
}