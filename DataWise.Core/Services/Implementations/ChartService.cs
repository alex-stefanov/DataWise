using System.Globalization;
using CsvHelper;
using DataWise.Common.Constants;
using DataWise.Common.DTOs;
using DataWise.Common.Helpers;
using DataWise.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using OpenAI_API;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace DataWise.Core.Services.Implementations;

/// <summary>
/// Implements chart generation functionalities using CSV data.
/// </summary>
public class ChartService(
    OpenAIAPI openAIAPI)
    : IChartService
{
    /// <inheritdoc />
    public async Task<string[]> ExtractColumnsAsync(
        IFormFile file)
    {
        if (file is null 
            || file.Length == 0)
            throw new ArgumentException("File is required.");

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (extension != ".csv")
            throw new NotSupportedException("Only CSV files are currently supported.");

        using var stream = file.OpenReadStream();
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        if (!await csv.ReadAsync() 
            || !csv.ReadHeader())
            throw new Exception("CSV file is missing a header.");

        return csv.HeaderRecord!;
    }

    /// <inheritdoc />
    public async Task<byte[]> GenerateChartAsync(
        ChartDto request,
        IFormFile file)
    {
        if (file is null 
            || file.Length == 0)
            throw new ArgumentException("File is required.");

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (extension != ".csv")
            throw new NotSupportedException("Only CSV files are currently supported.");

        if (request.ChartType != ChartType.Pie &&
            request.ChartType != ChartType.Line &&
            request.ChartType != ChartType.Bar)
            throw new NotSupportedException("Unsupported chart type.");

        var records = new List<Dictionary<string, string>>();
        using var stream = file.OpenReadStream();
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        await csv.ReadAsync();
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

        bool isNumeric = await ValidationHelper.IsColumnNumericAsync(
            records,
            request.ValueColumn,
            openAIAPI);

        if (!isNumeric)
        {
            throw new Exception($"The column '{request.ValueColumn}' is not recognized as numeric.");
        }

        var aggregatedData = ValidationHelper.ProcessDataAggregation(
            records, request.CategoryColumn, request.ValueColumn, request.Aggregation);

        var plotModel = new PlotModel
        {
            Title = request.Title
        };

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
        }

        foreach (var axis in plotModel.Axes)
        {
            if (axis is CategoryAxis categoryAxis)
            {
                categoryAxis.FontSize = 14;
                categoryAxis.Angle = 45;
            }
        }

        var svgExporter = new SvgExporter
        {
            Width = 1200,
            Height = 800
        };

        string svg = svgExporter.ExportToString(plotModel);
        return System.Text.Encoding.UTF8.GetBytes(svg);
    }
}