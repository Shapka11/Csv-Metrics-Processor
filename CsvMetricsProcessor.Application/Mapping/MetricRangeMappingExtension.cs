using CsvMetricsProcessor.Contracts.Models;
using CsvMetricsProcessor.Domain.Models;

namespace CsvMetricsProcessor.Application.Mapping;

public static class MetricRangeMappingExtension
{
    public static MetricRange MapFromDto(this MetricRangeDto metricRangeDto)
        => new MetricRange(metricRangeDto.Min, metricRangeDto.Max);
}