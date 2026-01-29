using System.Reflection;
using CsvMetricsProcessor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CsvMetricsProcessor.Infrastructure.Persistence;

public class MetricsDbContext : DbContext
{
    public MetricsDbContext(DbContextOptions<MetricsDbContext> options) : base(options) { }

    public DbSet<Measurement> Measurements { get; set; }

    public DbSet<Statistics> Statistics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}