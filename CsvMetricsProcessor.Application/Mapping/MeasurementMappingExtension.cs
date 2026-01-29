using CsvMetricsProcessor.Contracts.Models;
using CsvMetricsProcessor.Domain.Entities;

namespace CsvMetricsProcessor.Application.Mapping;

public static class MeasurementMappingExtension
{
    public static MeasurementDto MapToDto(this Measurement measurement)
        => new MeasurementDto(measurement.Date, measurement.ExecutionTime, measurement.Value);
}