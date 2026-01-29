using CsvMetricsProcessor.Domain.Entities;

namespace CsvMetricsProcessor.Domain.ResultTypes;

public abstract record MeasurementCreationResult
{
    private MeasurementCreationResult() { }

    public sealed record Success(Measurement Measurement) : MeasurementCreationResult;
    
    public sealed record Failure(string Error) :  MeasurementCreationResult;
}