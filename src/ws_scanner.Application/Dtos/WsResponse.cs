using System.Text.Json.Serialization;
using ws_scanner.Application.Dtos;

public static class WsResponse
{
    public record WsEnvelope(
        [property: JsonPropertyName("event")]
        string Event,

        [property: JsonPropertyName("cmd")]
        string Cmd,

        [property: JsonPropertyName("doc_type")]
        string? DocType,

        [property: JsonPropertyName("action_type")]
        string? ActionType,

        [property: JsonPropertyName("action_source")]
        string? ActionSource,

        [property: JsonPropertyName("success")]
        bool Success,

        [property: JsonPropertyName("message")]
        string? Message,

        [property: JsonPropertyName("data")]
        object? Data,

        [property: JsonPropertyName("timestamp")]
        DateTime Timestamp
    );

    // 🔥 READY
    public static WsEnvelope Ready(WsRequest req, object? data = null)
        => new(
            Event: "READY",
            Cmd: "response",
            DocType: req.DocType,
            ActionType: req.ActionType,
            ActionSource: req.ActionSource,
            Success: true,
            Message: "Ready",
            Data: data,
            Timestamp: DateTime.UtcNow
        );

    public static WsEnvelope ImgResult(WsRequest req, object? data = null)
    => new(
        Event: "IMG_RESULT",
        Cmd: "response",
        DocType: req.DocType,
        ActionType: req.ActionType,
        ActionSource: req.ActionSource,
        Success: true,
        Message: "IMG Success",
        Data: data,
        Timestamp: DateTime.UtcNow
    );

    // 🔥 OCR RESULT
    public static WsEnvelope OcrResult(WsRequest req, object data)
        => new(
            Event: "OCR_RESULT",
            Cmd: "response",
            DocType: req.DocType,
            ActionType: req.ActionType,
            ActionSource: req.ActionSource,
            Success: true,
            Message: "OCR Success",
            Data: data,
            Timestamp: DateTime.UtcNow
        );

    // 🔥 ERROR
    public static WsEnvelope Error(WsRequest? req, string message)
        => new(
            Event: "ERROR",
            Cmd: "response",
            DocType: req?.DocType,
            ActionType: req?.ActionType,
            ActionSource: req?.ActionSource,
            Success: false,
            Message: message,
            Data: null,
            Timestamp: DateTime.UtcNow
        );
}
