using System.Diagnostics;
using CsvMetricsProcessor.Contracts.Interfaces;
using CsvMetricsProcessor.Contracts.Operations;
using CsvMetricsProcessor.Presentation.Http.Models;
using Microsoft.AspNetCore.Mvc;

namespace CsvMetricsProcessor.Presentation.Http.Controllers;

[ApiController]
[Route("api/files")]
public sealed class FilesController : ControllerBase
{
    private readonly IFileProcessingService _fileProcessingService;

    public FilesController(IFileProcessingService fileProcessingService)
    {
        _fileProcessingService = fileProcessingService;
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<string>> Upload(
        [FromForm] UploadMetricsHttpRequest httpRequest,
        CancellationToken ct)
    {
        var stream = httpRequest.File.OpenReadStream();        
        var request = new UploadMetrics.Request(stream, httpRequest.File.FileName);

        UploadMetrics.Response response = await _fileProcessingService.UploadMetrics(request, ct);

        return response switch
        {
            UploadMetrics.Response.Success success => Ok(success.Message),
            UploadMetrics.Response.Failure failure => BadRequest(failure.ErrorMessage),
            _ => throw new UnreachableException(),
        };
    }
}