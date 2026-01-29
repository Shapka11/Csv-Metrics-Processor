using CsvMetricsProcessor.Domain.Entities;
using CsvMetricsProcessor.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CsvMetricsProcessor.Infrastructure.Persistence.Configurations;


public class StatisticsConfiguration : IEntityTypeConfiguration<Statistics>
{
    public void Configure(EntityTypeBuilder<Statistics> builder)
    {
        builder.ToTable("Results");

        builder.HasKey(r => r.FileName);

        builder.Property(r => r.FileName)
            .HasColumnName("FileName")
            .HasConversion(
                vo => vo.Value,
                dbValue => new ProcessingFileName(dbValue)
            );

        builder.Property(r => r.TimeDelta);
        builder.Property(r => r.MinDate);
        builder.Property(r => r.AvgExecutionTime);
        builder.Property(r => r.AvgValue);
        builder.Property(r => r.MedianValue);
        builder.Property(r => r.MaxValue);
        builder.Property(r => r.MinValue);

        builder.HasIndex(r => r.AvgValue);
        builder.HasIndex(r => r.MinDate);
    }
}