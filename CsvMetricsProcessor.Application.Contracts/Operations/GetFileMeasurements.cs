using CsvMetricsProcessor.Contracts.Models;

namespace CsvMetricsProcessor.Contracts.Operations;

public static class GetFileMeasurements
{
    public sealed record Request(string FileName, int Limit = 10);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(IEnumerable<MeasurementDto> Measurements) : Response;

        public sealed record Failure(string ErrorMessage) : Response;
    }
}