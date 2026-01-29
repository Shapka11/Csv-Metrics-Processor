using CsvMetricsProcessor.Abstractions;
using CsvMetricsProcessor.Abstractions.Repositories;
using CsvMetricsProcessor.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CsvMetricsProcessor.Infrastructure.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection collection)
    {
        collection.AddScoped<IPersistenceContext, PersistenceContext>();

        collection.AddScoped<IMeasurementRepository, MeasurementRepository>();
        collection.AddScoped<IStatisticsRepository, StatisticsRepository>();

        return collection;
    }
}