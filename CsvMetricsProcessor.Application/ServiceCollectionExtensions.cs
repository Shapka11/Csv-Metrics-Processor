using CsvMetricsProcessor.Application.Services;
using CsvMetricsProcessor.Contracts.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CsvMetricsProcessor.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IMetricsReadService, MetricsReadService>();
        collection.AddScoped<IFileProcessingService, FileProcessingService>();

        return collection;
    }
}