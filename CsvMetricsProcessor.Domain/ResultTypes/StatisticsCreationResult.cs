using CsvMetricsProcessor.Domain.Entities;

namespace CsvMetricsProcessor.Domain.ResultTypes;

public abstract record StatisticsCreationResult
{
    private StatisticsCreationResult() { }

    public sealed record Success(Statistics Statistics) : StatisticsCreationResult;

    public sealed record Failure(string Error) : StatisticsCreationResult;
}