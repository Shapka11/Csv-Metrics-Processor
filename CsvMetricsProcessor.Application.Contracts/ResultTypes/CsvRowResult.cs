using CsvMetricsProcessor.Domain.Entities;

namespace CsvMetricsProcessor.Contracts.ResultTypes;

public abstract record CsvRowResult
{
    private CsvRowResult() { }

    public sealed record Success(Measurement Measurement) : CsvRowResult;

    public sealed record Failure(string Error) : CsvRowResult;
}