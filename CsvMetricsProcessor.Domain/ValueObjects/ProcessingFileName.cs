namespace CsvMetricsProcessor.Domain.ValueObjects;

public sealed record ProcessingFileName
{
    public ProcessingFileName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("The file name cannot be empty");

        Value = name;
    }

    public string Value { get; }

    public override string ToString() => Value;
}