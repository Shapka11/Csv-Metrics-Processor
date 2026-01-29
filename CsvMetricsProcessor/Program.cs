using CsvMetricsProcessor.Application;
using CsvMetricsProcessor.Infrastructure.Persistence;
using CsvMetricsProcessor.Presentation.Http;


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure() 
    .AddPresentationHttp();

builder.Services.AddSwaggerGen().AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();