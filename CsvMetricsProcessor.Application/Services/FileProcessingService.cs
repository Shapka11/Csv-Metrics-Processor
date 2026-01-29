using CsvMetricsProcessor.Abstractions;
using CsvMetricsProcessor.Contracts.Interfaces;
using CsvMetricsProcessor.Contracts.Operations;
using CsvMetricsProcessor.Contracts.ResultTypes;
using CsvMetricsProcessor.Domain.Entities;
using CsvMetricsProcessor.Domain.ResultTypes;

namespace CsvMetricsProcessor.Application.Services;

public sealed class FileProcessingService : IFileProcessingService
{
    private readonly ICsvParser _csvParser;
    private readonly IPersistenceContext _context;

    public FileProcessingService(
        ICsvParser csvParser,
        IPersistenceContext context)
    {
        _csvParser = csvParser;
        _context = context;
    }

    public async Task<UploadMetrics.Response> UploadMetrics(UploadMetrics.Request request, CancellationToken ct = default)
    {
        var measurements = new List<Measurement>();
        
        foreach (var rowResult in _csvParser.Parse(request.Content, request.FileName))
        {
            if (rowResult is CsvRowResult.Failure failure)
            {
                return new UploadMetrics.Response.Failure(failure.Error);
            }

            if (rowResult is CsvRowResult.Success success)
            {
                measurements.Add(success.Measurement);
            }
        }

        if (measurements.Count is < 1 or > 10_000)
        {
            return new UploadMetrics.Response.Failure(
                $"The file must contain from 1 to 10,000 records. The current number: {measurements.Count}");
        }

        var processingFileName = measurements[0].FileName;

        var statsResult = Statistics.Create(processingFileName, measurements);

        if (statsResult is StatisticsCreationResult.Failure statsFailure)
        {
             return new UploadMetrics.Response.Failure(statsFailure.Error);
        }

        var statistics = ((StatisticsCreationResult.Success)statsResult).Statistics;

        await using var transaction = await _context.BeginTransactionAsync(ct);

        await _context.Measurements.DeleteByFileName(processingFileName, ct);
        await _context.Statistics.DeleteByFileName(processingFileName, ct);
        
        await _context.Statistics.Add(statistics, ct);
        await _context.Measurements.Add(measurements, ct);

        await _context.SaveChangesAsync(ct);

        await transaction.CommitAsync(ct);
        
        return new UploadMetrics.Response.Success("The file has been successfully processed and saved.");
    }
}