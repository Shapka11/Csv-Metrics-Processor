using CsvMetricsProcessor.Domain.ValueObjects;
using SourceKit.Generators.Builder.Annotations;

namespace CsvMetricsProcessor.Abstractions.Queries;

[GenerateBuilder]
public sealed partial record MeasurementQuery(
    ProcessingFileName[] FileNames,
    int Limit = 10);