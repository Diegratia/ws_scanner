using System;

namespace ws_scanner.Application.Dtos
{
    public static class WsResponse
    {

        public record WsEnvelope(
            string Event,
            string? Type,
            bool Success,
            string? Message,
            object? Data,
            DateTime Timestamp
        );
        public static WsEnvelope Ready(string type, object? data = null)
            => new(
                Event: "READY",
                Type: type,
                Success: true,
                Message: "Ready",
                Data: data,
                Timestamp: DateTime.UtcNow
            );

        public static WsEnvelope OcrResult(string type, object data)
            => new(
                Event: "OCR_RESULT",
                Type: type,
                Success: true,
                Message: "OCR Success",
                Data: data,
                Timestamp: DateTime.UtcNow
            );

        public static WsEnvelope Error(string message)
            => new(
                Event: "ERROR",
                Type: null,
                Success: false,
                Message: message,
                Data: null,
                Timestamp: DateTime.UtcNow
            );
    }

}