using CsvMetricsProcessor.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace CsvMetricsProcessor.Abstractions;

public interface IPersistenceContext
{
    IMeasurementRepository Measurements { get; }
    
    IStatisticsRepository Statistics { get; }
    
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default);

    Task SaveChangesAsync(CancellationToken ct = default);
}