namespace CsvMetricsProcessor.Contracts.Models;

public sealed record MeasurementDto(DateTime Date, double ExecutionTime, double Value);