using System.Globalization;
using CsvHelper;
using DataWise.Common.Constants;
using DataWise.Common.DTOs;
using DataWise.Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace DataWise.Api.Controllers;

[ApiController]
[Route("api/chart")]
public class ChartController
    : Microsoft.AspNetCore.Mvc.ControllerBase
{
    /// <summary>
    /// Extracts the header (columns) from a CSV file.
    /// </summary>
    [HttpPost("columns")]
    public async Task<IActionResult> ExtractColumns(
        IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File is required.");
        }

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (extension != ".csv")
        {
            return StatusCode(StatusCodes.Status501NotImplemented, "Only CSV files are currently supported.");
        }

        using var stream = file.OpenReadStream();
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        if (!await csv.ReadAsync()
            || !csv.ReadHeader())
        {
            return BadRequest("CSV file is missing a header.");
        }

        var headers = csv.HeaderRecord;
        return Ok(new { Columns = headers });
    }

    /// <summary>
    /// Generates a chart image based on the CSV file and provided chart configuration.
    /// </summary>
    [HttpPost("generate")]
    public async Task<IActionResult> GenerateChart(
        [FromForm] 
        ChartDto request,
        IFormFile file)
    {
        if (file == null 
            || file.Length == 0)
        {
            return BadRequest("File is required.");
        }

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (extension != ".csv")
        {
            return StatusCode(StatusCodes.Status501NotImplemented, "Only CSV files are currently supported.");
        }

        switch (request.ChartType)
        {
            case ChartType.Pie:
            case ChartType.Line:
            case ChartType.Bar:
                {
                    break;
                }
            default:
                {
                    return BadRequest("Unsupported chart type.");
                }
        }

        var records = new List<Dictionary<string, string>>();

        using var stream = file.OpenReadStream();
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Read();
        csv.ReadHeader();

        while (await csv.ReadAsync())
        {
            var record = new Dictionary<string, string>
            {
                [request.CategoryColumn] = csv.GetField(request.CategoryColumn)!,
                [request.ValueColumn] = csv.GetField(request.ValueColumn)!
            };

            records.Add(record);
        }

        string categoryColumn = request.CategoryColumn;
        string valueColumn = request.ValueColumn;

        var aggregatedData = DataHelper.ProcessDataAggregation(records, categoryColumn, valueColumn, request.Aggregation);

        var plotModel = new PlotModel { Title = request.Title };

        switch (request.ChartType)
        {
            case ChartType.Line:
                {
                    var lineSeries = new LineSeries();
                    int index = 0;

                    foreach (var (Category, AggregatedValue) in aggregatedData)
                    {
                        lineSeries.Points.Add(new DataPoint(index++, AggregatedValue));
                    }

                    plotModel.Series.Add(lineSeries);

                    plotModel.Axes.Add(new CategoryAxis
                    {
                        Position = AxisPosition.Bottom,
                        ItemsSource = aggregatedData.Select(d => d.Category).ToList()
                    });

                    break;
                }
            case ChartType.Bar:
                {
                    var barSeries = new BarSeries();

                    foreach (var (Category, AggregatedValue) in aggregatedData)
                    {
                        barSeries.Items.Add(new BarItem(AggregatedValue));
                    }

                    plotModel.Series.Add(barSeries);

                    plotModel.Axes.Add(new CategoryAxis
                    {
                        Position = AxisPosition.Left,
                        ItemsSource = aggregatedData.Select(d => d.Category).ToList()
                    });

                    break;
                }
            case ChartType.Pie:
                {
                    var pieSeries = new PieSeries();

                    foreach (var (Category, AggregatedValue) in aggregatedData)
                    {
                        pieSeries.Slices.Add(new PieSlice(Category, AggregatedValue));
                    }

                    plotModel.Series.Add(pieSeries);

                    break;
                }
            default:
                {
                    return BadRequest("Unsupported chart type.");
                }
        }

        using var chartStream = new MemoryStream();

        var pngExporter = new OxyPlot.SkiaSharp.PngExporter
        {
            Width = 1200,
            Height = 800
        };

        foreach (var axis in plotModel.Axes)
        {
            if (axis is CategoryAxis categoryAxis)
            {
                categoryAxis.FontSize = 14;
                categoryAxis.Angle = 45;
            }
        }

        pngExporter.Export(plotModel, chartStream);
        chartStream.Seek(0, SeekOrigin.Begin);

        return File(chartStream.ToArray(), "image/png");
    }
}