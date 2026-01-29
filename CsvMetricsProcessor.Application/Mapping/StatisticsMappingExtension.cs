using CsvMetricsProcessor.Contracts.Models;
using CsvMetricsProcessor.Domain.Entities;

namespace CsvMetricsProcessor.Application.Mapping;

public static class StatisticsMappingExtension
{
    public static StatisticsDto MapToDto(this Statistics statistics)
        => new StatisticsDto(
            statistics.FileName.Value,
            statistics.TimeDelta,
            statistics.MinDate,
            statistics.AvgExecutionTime,
            statistics.AvgValue,
            statistics.MedianValue,
            statistics.MaxValue,
            statistics.MinValue);
}