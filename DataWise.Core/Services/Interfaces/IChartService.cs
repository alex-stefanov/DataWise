using DataWise.Common.DTOs;
using Microsoft.AspNetCore.Http;

namespace DataWise.Core.Services.Interfaces;

/// <summary>
/// Provides methods for processing CSV files and generating chart images.
/// </summary>
public interface IChartService
{
    /// <summary>
    /// Extracts the column headers from a CSV file.
    /// </summary>
    /// <param name="file">The CSV file.</param>
    /// <returns>An array of column names.</returns>
    Task<string[]> ExtractColumnsAsync(
        IFormFile file);

    /// <summary>
    /// Generates a chart image based on the provided CSV file and chart configuration.
    /// </summary>
    /// <param name="request">The chart configuration details.</param>
    /// <param name="file">The CSV file.</param>
    /// <returns>The generated chart image as a byte array.</returns>
    Task<byte[]> GenerateChartAsync(
        ChartDto request,
        IFormFile file);
}