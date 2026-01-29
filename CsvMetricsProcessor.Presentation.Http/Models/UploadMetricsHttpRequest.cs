using Microsoft.AspNetCore.Http;

namespace CsvMetricsProcessor.Presentation.Http.Models;

public sealed class UploadMetricsHttpRequest
{
    public required IFormFile File { get; set; }
}