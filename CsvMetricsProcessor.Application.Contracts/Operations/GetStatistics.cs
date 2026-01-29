using CsvMetricsProcessor.Contracts.Models;

namespace CsvMetricsProcessor.Contracts.Operations;

public static class GetStatistics
{
    public sealed record Request(
        string? FileName,
        DateRangeDto? ExecutionDateRange,
        MetricRangeDto? AvgValueRange,
        MetricRangeDto? AvgExecutionTimeRange
    );

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(IEnumerable<StatisticsDto> Statistics) : Response;

        public sealed record Failure(string ErrorMessage) : Response;
    }
}