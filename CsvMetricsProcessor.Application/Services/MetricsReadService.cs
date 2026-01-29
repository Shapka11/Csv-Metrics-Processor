using CsvMetricsProcessor.Abstractions;
using CsvMetricsProcessor.Abstractions.Queries;
using CsvMetricsProcessor.Application.Mapping;
using CsvMetricsProcessor.Contracts.Interfaces;
using CsvMetricsProcessor.Contracts.Operations;
using CsvMetricsProcessor.Domain.ValueObjects;

namespace CsvMetricsProcessor.Application.Services;

public sealed class MetricsReadService : IMetricsReadService
{
    private readonly IPersistenceContext _context;

    public MetricsReadService(IPersistenceContext context)
    {
        _context = context;
    }

    public async Task<GetStatistics.Response> GetStatistics(
        GetStatistics.Request request, CancellationToken ct = default)
    {
        var fileName = new ProcessingFileName(request.FileName);
        var executionDateRange = request.ExecutionDateRange.MapFromDto();
        var avgValueRange = request.AvgValueRange.MapFromDto();
        var avgExecutionTimeRange = request.AvgExecutionTimeRange.MapFromDto();
        
        var query = StatisticsQuery.Build(builder => builder
            .WithFileName(fileName)
            .WithExecutionDateRange(executionDateRange)
            .WithAvgValueRange(avgValueRange)
            .WithAvgExecutionTimeRange(avgExecutionTimeRange)
        );

        var statisticsEntities = await _context.Statistics
            .Query(query, ct);

        var dtos = statisticsEntities
            .Select(s => s.MapToDto()) 
            .ToList();

        return new GetStatistics.Response.Success(dtos);
    }

    public async Task<GetFileMeasurements.Response> GetLastMeasurements(
        GetFileMeasurements.Request request, CancellationToken ct = default)
    {
        var fileName = new ProcessingFileName(request.FileName);
        const int limit = 10;
        
        var query = MeasurementQuery.Build(builder => builder
            .WithFileName(fileName)
            .WithLimit(limit)
        );

        var measurements = await _context.Measurements
            .Query(query, ct);

        var dtos = measurements
            .Select(m => m.MapToDto())
            .ToList();

        return new GetFileMeasurements.Response.Success(dtos);
    }
}