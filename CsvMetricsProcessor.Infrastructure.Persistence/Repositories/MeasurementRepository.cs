using CsvMetricsProcessor.Abstractions.Queries;
using CsvMetricsProcessor.Abstractions.Repositories;
using CsvMetricsProcessor.Domain.Entities;
using CsvMetricsProcessor.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CsvMetricsProcessor.Infrastructure.Persistence.Repositories;

public sealed class MeasurementRepository : IMeasurementRepository
{
    private readonly MetricsDbContext _dbContext;

    public MeasurementRepository(MetricsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task DeleteByFileName(ProcessingFileName fileName, CancellationToken ct = default)
    {
        return _dbContext.Measurements
            .Where(m => m.FileName == fileName)
            .ExecuteDeleteAsync(ct);
    }

    public Task Add(IEnumerable<Measurement> measurements, CancellationToken ct = default)
    {
        return _dbContext.Measurements.AddRangeAsync(measurements, ct);
    }

    public async Task<IEnumerable<Measurement>> Query(MeasurementQuery query, CancellationToken ct = default)
    {
        return await _dbContext.Measurements
            .AsNoTracking()
            .Where(m => query.FileNames.Contains(m.FileName))
            .OrderByDescending(m => m.Date)
            .Take(query.Limit)
            .ToListAsync(ct);
    }
}