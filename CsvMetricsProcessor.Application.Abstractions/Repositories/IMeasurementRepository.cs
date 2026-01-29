using CsvMetricsProcessor.Abstractions.Queries;
using CsvMetricsProcessor.Domain.Entities;
using CsvMetricsProcessor.Domain.ValueObjects;

namespace CsvMetricsProcessor.Abstractions.Repositories;

public interface IMeasurementRepository
{
    Task DeleteByFileName(ProcessingFileName fileName, CancellationToken ct = default);

    Task Add(IEnumerable<Measurement> measurements, CancellationToken ct = default);

    Task<IEnumerable<Measurement>> Query(MeasurementQuery query, CancellationToken ct = default);
}