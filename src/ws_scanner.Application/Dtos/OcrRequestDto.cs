using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ws_scanner.Application.Dtos
{

    public record OcrRequest(
    string ImagePath,
    string DocumentType
);

    public record OcrBulkItem(
    string ImageName,
    bool Success,
    object? Result,
    string? Error
);

    public record OcrBulkResult(
        int Total,
        List<OcrBulkItem> Items
    );

}
