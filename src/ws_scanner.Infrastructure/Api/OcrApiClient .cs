using System.Net.Http;
using ws_scanner.Application.Interfaces;

namespace ws_scanner.Infrastructure.Api
{
    public class OcrApiClient : IOcrApiClient
    {
        private readonly HttpClient _client = new();

        public async Task<string> SendAsync(string imagePath, string type)
        {
            var url = type == "passport"
                ? "http://localhost:5000/ocr-passport"
                : "http://localhost:5000/ocr-ktp";

            using var form = new MultipartFormDataContent();
            form.Add(
                new StreamContent(File.OpenRead(imagePath)),
                "image",
                Path.GetFileName(imagePath)
            );

            var response = await _client.PostAsync(url, form);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }

}
