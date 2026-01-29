using FluentMigrator;

namespace CsvMetricsProcessor.Infrastructure.Persistence.Migrations;

[Migration(1, "Initial")]
public class Initial : Migration
{
    public override void Up()
    {
        Execute.Sql(@"
            create table public.""Results""
            (
                file_name text primary key,
                time_delta double precision not null,
                min_date timestamp without time zone not null,
                avg_execution_time double precision not null,
                avg_value double precision not null,
                median_value double precision not null,
                max_value double precision not null,
                min_value double precision not null
            );

            create table public.""Values""
            (
                id uuid primary key,
                file_name text not null,
                date timestamp without time zone not null,
                execution_time double precision not null,
                value double precision not null,

                constraint fk_values_results foreign key (file_name)
                    references public.""Results"" (file_name)
                    on delete cascade
            );

            create index idx_values_filename_date on public.""Values"" (file_name, date desc);
            
            create index idx_results_min_date on public.""Results"" (min_date);
            create index idx_results_avg_value on public.""Results"" (avg_value);
            create index idx_results_avg_time on public.""Results"" (avg_execution_time);
        ");
    }

    public override void Down()
    {
        Execute.Sql(@"
            drop index if exists public.idx_results_avg_time;
            drop index if exists public.idx_results_avg_value;
            drop index if exists public.idx_results_min_date;
            drop index if exists public.idx_values_filename_date;

            drop table if exists public.""Values"";
            drop table if exists public.""Results"";
        ");
    }
}