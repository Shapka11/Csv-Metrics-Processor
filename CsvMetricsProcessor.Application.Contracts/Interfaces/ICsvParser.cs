using CsvMetricsProcessor.Contracts.ResultTypes;

namespace CsvMetricsProcessor.Contracts.Interfaces;

public interface ICsvParser
{
    IEnumerable<CsvRowResult> Parse(Stream stream, string fileName);
}