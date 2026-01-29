using System.Diagnostics;
using CsvMetricsProcessor.Contracts.Interfaces;
using CsvMetricsProcessor.Contracts.Models;
using CsvMetricsProcessor.Contracts.Operations;
using CsvMetricsProcessor.Presentation.Http.Models;
using Microsoft.AspNetCore.Mvc;

namespace CsvMetricsProcessor.Presentation.Http.Controllers;

[ApiController]
[Route("api/metrics")]
public sealed class MetricsController : ControllerBase
{
    private readonly IMetricsReadService _metricsReadService;

    public MetricsController(IMetricsReadService metricsReadService)
    {
        _metricsReadService = metricsReadService;
    }

    [HttpPost("statistics")]
    public async Task<ActionResult<IEnumerable<StatisticsDto>>> GetStatistics(
        [FromBody] GetStatisticsHttpRequest httpRequest, 
        CancellationToken ct)
    {
        var request = new GetStatistics.Request(
            FileName: httpRequest.FileName is not null 
                ? httpRequest.FileName
                : string.Empty,

            ExecutionDateRange: httpRequest.ExecutionDateRange is not null 
                ? httpRequest.ExecutionDateRange 
                : null,

            AvgValueRange: httpRequest.AvgValueRange is not null 
                ? httpRequest.AvgValueRange
                : null,

            AvgExecutionTimeRange: httpRequest.AvgExecutionTimeRange is not null 
                ? httpRequest.AvgExecutionTimeRange
                : null
        );

        var response = await _metricsReadService.GetStatistics(request, ct);

        return response switch
        {
            GetStatistics.Response.Success success => Ok(success.Statistics),
            GetStatistics.Response.Failure failure => BadRequest(failure.ErrorMessage),
            _ => throw new UnreachableException()
        };
    }

    [HttpPost("measurements")]
    public async Task<ActionResult<IEnumerable<MeasurementDto>>> GetMeasurements(
        [FromBody] GetMeasurementsHttpRequest httpRequest, CancellationToken ct)
    {
        var request = new GetFileMeasurements.Request(httpRequest.FileName);

        GetFileMeasurements.Response response = await _metricsReadService.GetLastMeasurements(request, ct);

        return response switch
        {
            GetFileMeasurements.Response.Success success => Ok(success.Measurements),
            GetFileMeasurements.Response.Failure failure => BadRequest(failure.ErrorMessage),
            _ => throw new UnreachableException(), 
        };
    }
}