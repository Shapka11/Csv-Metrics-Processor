using CsvMetricsProcessor.Abstractions.Queries;
using CsvMetricsProcessor.Domain.Entities;
using CsvMetricsProcessor.Domain.ValueObjects;

namespace CsvMetricsProcessor.Abstractions.Repositories;

public interface IStatisticsRepository
{
    Task Add(Statistics stats, CancellationToken ct = default);

    Task DeleteByFileName(ProcessingFileName fileName, CancellationToken ct = default);

    Task<IEnumerable<Statistics>> Query(StatisticsQuery query, CancellationToken ct = default);
}