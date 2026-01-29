namespace CsvMetricsProcessor.Presentation.Http.Models;

public sealed class GetMeasurementsHttpRequest
{
    public required string FileName { get; set; }
}