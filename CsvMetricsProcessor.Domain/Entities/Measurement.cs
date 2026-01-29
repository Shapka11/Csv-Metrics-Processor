using CsvMetricsProcessor.Domain.ResultTypes;
using CsvMetricsProcessor.Domain.ValueObjects;

namespace CsvMetricsProcessor.Domain.Entities;

public sealed class Measurement
{
    private Measurement(ProcessingFileName fileName, DateTime date, double executionTime, double value)
    {
        Id = Guid.NewGuid();
        FileName = fileName;
        Date = date;
        ExecutionTime = executionTime;
        Value = value;
    }

    public Guid Id { get; private set; }

    public ProcessingFileName FileName { get; private set; }

    public DateTime Date { get; private set; }

    public double ExecutionTime { get; private set; }

    public double Value { get; private set; }

    public static MeasurementCreationResult Create(
        ProcessingFileName fileName,
        DateTime date,
        double executionTime,
        double value,
        int rowLine)
    {
        if (date < new DateTime(2000, 1, 1) || date > DateTime.UtcNow)
            return new MeasurementCreationResult.Failure($"String {rowLine}: Incorrect date {date:d}");

        if (executionTime < 0)
            return new MeasurementCreationResult.Failure($"String {rowLine}: Execution time less than 0");

        if (value < 0)
            return new MeasurementCreationResult.Failure($"String {rowLine}: Value less than 0");

        var entity = new Measurement(fileName, date, executionTime, value);

        return new MeasurementCreationResult.Success(entity);
    }
}