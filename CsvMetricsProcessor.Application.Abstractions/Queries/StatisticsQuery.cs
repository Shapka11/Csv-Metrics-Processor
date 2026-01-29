using CsvMetricsProcessor.Domain.Models;
using CsvMetricsProcessor.Domain.ValueObjects;
using SourceKit.Generators.Builder.Annotations;

namespace CsvMetricsProcessor.Abstractions.Queries;

[GenerateBuilder]
public sealed partial record StatisticsQuery(
    ProcessingFileName[] FileNames,
    DateRange[] ExecutionDateRanges,
    MetricRange[] AvgValueRanges,
    MetricRange[] AvgExecutionTimeRanges);