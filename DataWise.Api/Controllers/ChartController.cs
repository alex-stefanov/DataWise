using Microsoft.AspNetCore.Mvc;
using DTOS = DataWise.Common.DTOs;
using INTERFACES = DataWise.Core.Services.Interfaces;

namespace DataWise.Api.Controllers;

/// <summary>
/// Controller responsible for handling chart-related endpoints.
/// </summary>
/// <param name="chartService">The chart service instance.</param>
[Route("api/chart")]
[ApiController]
public class ChartController(
    INTERFACES.IChartService chartService)
    : ControllerBase
{
    /// <summary>
    /// Extracts column headers from a CSV file.
    /// </summary>
    /// <param name="file">The CSV file containing data.</param>
    /// <returns>A list of column names.</returns>
    [HttpPost("columns")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    public async Task<IActionResult> ExtractColumns(
        IFormFile file)
    {
        try
        {
            var columns = await chartService
                .ExtractColumnsAsync(file);

            return Ok(new { Columns = columns });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (NotSupportedException ex)
        {
            return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Generates a chart image based on a CSV file and provided chart configuration.
    /// </summary>
    /// <param name="request">The chart configuration details.</param>
    /// <param name="file">The CSV file containing data.</param>
    /// <returns>The generated chart image in PNG format.</returns>
    [HttpPost("generate")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    public async Task<IActionResult> GenerateChart(
        [FromForm]
        DTOS.ChartDto request,
        IFormFile file)
    {
        try
        {
            var imageBytes = await chartService
                .GenerateChartAsync(request, file);

            return File(imageBytes, "image/svg+xml");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (NotSupportedException ex)
        {
            return StatusCode(StatusCodes.Status501NotImplemented, ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}