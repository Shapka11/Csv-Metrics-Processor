namespace CsvMetricsProcessor.Contracts.Operations;

public static class UploadMetrics
{
    public readonly record struct Request(Stream Content, string FileName);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(string Message) : Response;

        public sealed record Failure(string ErrorMessage) : Response;
    }
}