using CsvMetricsProcessor.Contracts.Operations;

namespace CsvMetricsProcessor.Contracts.Interfaces;

public interface IFileProcessingService
{
    Task<UploadMetrics.Response> UploadMetrics(UploadMetrics.Request request, CancellationToken ct = default);
}