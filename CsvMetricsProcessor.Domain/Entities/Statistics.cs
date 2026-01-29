using CsvMetricsProcessor.Domain.ResultTypes;
using CsvMetricsProcessor.Domain.ValueObjects;

namespace CsvMetricsProcessor.Domain.Entities;

public sealed class Statistics
{
    private Statistics(ProcessingFileName fileName)
    {
        FileName = fileName;
    }

    public ProcessingFileName FileName { get; private set; }

    public double TimeDelta { get; private set; }

    public DateTime MinDate { get; private set; }

    public double AvgExecutionTime { get; private set; }

    public double AvgValue { get; private set; }

    public double MedianValue { get; private set; }

    public double MaxValue { get; private set; }

    public double MinValue { get; private set; }

    public static StatisticsCreationResult Create(ProcessingFileName fileName, List<Measurement> measurements)
    {
        if (measurements.Count < 1 || measurements.Count > 10000)
        {
            return new StatisticsCreationResult.Failure(
                $"File {fileName.Value} contains {measurements.Count} rows. " +
                "Allowed range is from 1 to 10,000.");
        }

        var result = new Statistics(fileName);

        var sortedByDate = measurements.OrderBy(m => m.Date).ToList();
        var sortedByValue = measurements.OrderBy(m => m.Value).ToList();

        result.MinDate = sortedByDate.First().Date;
        var maxDate = sortedByDate.Last().Date;
        result.TimeDelta = (maxDate - result.MinDate).TotalSeconds;

        result.AvgExecutionTime = measurements.Average(m => m.ExecutionTime);
        result.AvgValue = measurements.Average(m => m.Value);
        result.MinValue = sortedByValue.First().Value;
        result.MaxValue = sortedByValue.Last().Value;

        int count = sortedByValue.Count;
        if (count % 2 == 0)
            result.MedianValue = (sortedByValue[count / 2 - 1].Value + sortedByValue[count / 2].Value) / 2.0;
        else
            result.MedianValue = sortedByValue[count / 2].Value;

        return new StatisticsCreationResult.Success(result);
    }
}