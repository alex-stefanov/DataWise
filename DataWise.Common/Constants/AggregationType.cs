namespace DataWise.Common.Constants;

/// <summary>
/// Defines the different types of aggregations that can be applied to data.
/// </summary>
public enum AggregationType
{
    /// <summary>
    /// Represents the total sum of all values.
    /// </summary>
    Total = 0,

    /// <summary>
    /// Represents the average (mean) value.
    /// </summary>
    Average = 1,

    /// <summary>
    /// Represents the minimum value.
    /// </summary>
    Minimum = 2,

    /// <summary>
    /// Represents the maximum value.
    /// </summary>
    Maximum = 3
}