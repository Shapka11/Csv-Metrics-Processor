using CsvMetricsProcessor.Domain.Entities;
using CsvMetricsProcessor.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CsvMetricsProcessor.Infrastructure.Persistence.Configurations;

public class MeasurementConfiguration : IEntityTypeConfiguration<Measurement>
{    
    public void Configure(EntityTypeBuilder<Measurement> builder)
    {
        builder.ToTable("Values");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.FileName)
            .HasColumnName("file_name")
            .IsRequired()
            .HasConversion(
                vo => vo.Value,
                dbValue => new ProcessingFileName(dbValue) 
            );

        builder.Property(m => m.Date)
            .HasColumnName("date")
            .IsRequired();

        builder.Property(m => m.ExecutionTime)
            .HasColumnName("execution_time")
            .IsRequired();

        builder.Property(m => m.Value)
            .HasColumnName("value")
            .IsRequired();

        builder.HasIndex(m => new { m.FileName, m.Date });
    }
}