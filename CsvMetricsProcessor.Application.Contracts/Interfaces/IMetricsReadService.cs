using CsvMetricsProcessor.Contracts.Operations;

namespace CsvMetricsProcessor.Contracts.Interfaces;

public interface IMetricsReadService
{
    Task<GetStatistics.Response> GetStatistics(GetStatistics.Request request, CancellationToken ct = default);

    Task<GetFileMeasurements.Response> GetLastMeasurements(
        GetFileMeasurements.Request request, CancellationToken ct = default);
}