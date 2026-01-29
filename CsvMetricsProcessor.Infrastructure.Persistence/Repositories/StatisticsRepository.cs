using CsvMetricsProcessor.Abstractions.Queries;
using CsvMetricsProcessor.Abstractions.Repositories;
using CsvMetricsProcessor.Domain.Entities;
using CsvMetricsProcessor.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CsvMetricsProcessor.Infrastructure.Persistence.Repositories;

public sealed class StatisticsRepository : IStatisticsRepository
{
    private readonly MetricsDbContext _dbContext;

    public StatisticsRepository(MetricsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Add(Statistics stats, CancellationToken ct = default)
    {
        return _dbContext.Statistics.AddAsync(stats, ct).AsTask();
    }
    
    public Task DeleteByFileName(ProcessingFileName fileName, CancellationToken ct = default)
    {
        return _dbContext.Statistics
            .Where(s => s.FileName == fileName)
            .ExecuteDeleteAsync(ct);
    }

    public async Task<IEnumerable<Statistics>> Query(StatisticsQuery query, CancellationToken ct = default)
    {
        var dbQuery = _dbContext.Statistics.AsNoTracking();

        if (query.FileNames is { Length: > 0 })
        {
            dbQuery = dbQuery.Where(s => query.FileNames.Contains(s.FileName));
        }

        if (query.ExecutionDateRanges is { Length: > 0 })
        {
            var ranges = query.ExecutionDateRanges;

            dbQuery = dbQuery.Where(s => 
                ranges.Any(r => s.MinDate >= r.From && s.MinDate <= r.To));
        }

        if (query.AvgValueRanges is { Length: > 0 })
        {
            var ranges = query.AvgValueRanges;

            dbQuery = dbQuery.Where(s => 
                ranges.Any(r => s.AvgValue >= r.Min && s.AvgValue <= r.Max));
        }

        if (query.AvgExecutionTimeRanges is { Length: > 0 })
        {
            var ranges = query.AvgExecutionTimeRanges;

            dbQuery = dbQuery.Where(s => 
                ranges.Any(r => s.AvgExecutionTime >= r.Min && s.AvgExecutionTime <= r.Max));
        }

        return await dbQuery.ToListAsync(ct);
    }

}