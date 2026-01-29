namespace CsvMetricsProcessor.Contracts.Models;

public sealed record StatisticsDto(
    string FileName,
    double TimeDelta,
    DateTime MinDate,
    double AvgExecutionTime,
    double AvgValue,
    double MedianValue,
    double MaxValue,
    double MinValue
);