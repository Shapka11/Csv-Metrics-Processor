using CsvMetricsProcessor.Contracts.Models;
using CsvMetricsProcessor.Domain.Models;

namespace CsvMetricsProcessor.Application.Mapping;

public static class DateRangeMappingExtension
{
    public static DateRange MapFromDto(this DateRangeDto dateRangeDto)
        => new DateRange(dateRangeDto.From, dateRangeDto.To);
}