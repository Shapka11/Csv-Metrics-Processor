using CsvMetricsProcessor.Contracts.Models;

namespace CsvMetricsProcessor.Presentation.Http.Models;

public sealed class GetStatisticsHttpRequest
{
    public string? FileName { get; set; }

    public DateRangeDto? ExecutionDateRange { get; set; }

    public MetricRangeDto? AvgValueRange { get; set; }

    public MetricRangeDto? AvgExecutionTimeRange { get; set; }
}