using DataWise.Common.Constants;

namespace DataWise.Common.DTOs;

public class ChartDto
{
    public required string Title { get; set; }

    public required string CategoryColumn { get; set; }

    public required string ValueColumn { get; set; }

    public ChartType ChartType { get; set; }

    public AggregationType Aggregation { get; set; }
}