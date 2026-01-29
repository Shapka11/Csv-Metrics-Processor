using CsvMetricsProcessor.Abstractions;
using CsvMetricsProcessor.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace CsvMetricsProcessor.Infrastructure.Persistence;

public sealed class PersistenceContext : IPersistenceContext
{
    private readonly MetricsDbContext _dbContext;

    public PersistenceContext(
        MetricsDbContext dbContext,
        IMeasurementRepository measurementRepository,
        IStatisticsRepository statisticsRepository)
    {
        _dbContext = dbContext;
        Measurements = measurementRepository;
        Statistics = statisticsRepository;
    }

    public IMeasurementRepository Measurements { get; }

    public IStatisticsRepository Statistics { get; }
    
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default)
    {
        return _dbContext.Database.BeginTransactionAsync(ct);
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        return _dbContext.SaveChangesAsync(ct);
    }
}