using System.Globalization;
using CsvMetricsProcessor.Contracts.Interfaces;
using CsvMetricsProcessor.Contracts.ResultTypes;
using CsvMetricsProcessor.Domain.Entities;
using CsvMetricsProcessor.Domain.ResultTypes;
using CsvMetricsProcessor.Domain.ValueObjects;

namespace CsvMetricsProcessor.Application.Services;

public sealed class CsvParser : ICsvParser
{
    public IEnumerable<CsvRowResult> Parse(Stream stream, string fileName)
    {
        using var reader = new StreamReader(stream);
        var lineCount = 0;
        var processingFileName = new ProcessingFileName(fileName);

        while (reader.ReadLine() is { } line)
        {
            lineCount++;

            if (lineCount == 1 && (line.Contains("Date") || line.Contains("Value"))) continue;

            var parts = line.Split(';');

            if (parts.Length < 3)
            {
                yield return new CsvRowResult.Failure(
                    $"Line {lineCount}: Invalid column count. Expected 3, got {parts.Length}.");
                continue;
            }

            if (DateTime.TryParse(parts[0].Trim(), out var date) is false)
            {
                yield return new CsvRowResult.Failure(
                    $"Line {lineCount}: Invalid date format '{parts[0]}'.");
                continue;
            }

            if (double.TryParse(parts[1].Trim(), CultureInfo.InvariantCulture, out var executionTime) is false)
            {
                yield return new CsvRowResult.Failure(
                    $"Line {lineCount}: Invalid execution time format '{parts[1]}'.");
                continue;
            }

            if (double.TryParse(parts[2].Trim(), CultureInfo.InvariantCulture, out var value) is false)
            {
                yield return new CsvRowResult.Failure(
                    $"Line {lineCount}: Invalid value format '{parts[2]}'.");
                continue;
            }

            var domainResult = Measurement.Create(
                processingFileName,
                date,
                executionTime,
                value,
                lineCount);

            if (domainResult is MeasurementCreationResult.Success success)
            {
                yield return new CsvRowResult.Success(success.Measurement);
                continue;
            }

            if (domainResult is MeasurementCreationResult.Failure failure)
            {
                yield return new CsvRowResult.Failure(
                    $"Line {lineCount}: Domain validation failed - {failure.Error}");
            }
        }
    }
}