using CONSTANTS = DataWise.Common.Constants;

namespace DataWise.Common.DTOs;

/// <summary>
/// Represents the data transfer object (DTO) for chart configuration.
/// </summary>
public class ChartDto
{
    /// <summary>
    /// Gets or sets the title of the chart.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the name of the column used for categorization in the chart.
    /// </summary>
    public required string CategoryColumn { get; set; }

    /// <summary>
    /// Gets or sets the name of the column used for values in the chart.
    /// </summary>
    public required string ValueColumn { get; set; }

    /// <summary>
    /// Gets or sets the type of chart to be generated.
    /// </summary>
    public CONSTANTS.ChartType ChartType { get; set; }

    /// <summary>
    /// Gets or sets the type of aggregation applied to the data.
    /// </summary>
    public CONSTANTS.AggregationType Aggregation { get; set; }
}