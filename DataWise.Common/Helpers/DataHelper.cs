using CONSTANTS = DataWise.Common.Constants;

namespace DataWise.Common.Helpers;

/// <summary>
/// Provides helper methods for processing and aggregating data.
/// </summary>
public static class DataHelper
{
    /// <summary>
    /// Groups records by the specified category column and applies the given aggregation 
    /// function on the numeric value column.
    /// </summary>
    /// <param name="records">The list of records represented as dictionaries with column names as keys.</param>
    /// <param name="categoryColumn">The name of the column to group data by.</param>
    /// <param name="valueColumn">The name of the column containing numeric values for aggregation.</param>
    /// <param name="aggregation">The type of aggregation to apply (Total, Average, Minimum, Maximum).</param>
    /// <returns>A list of tuples where each tuple contains a category and its corresponding aggregated value.</returns>
    public static List<(string Category, double AggregatedValue)> ProcessDataAggregation(
        List<Dictionary<string, string>> records,
        string categoryColumn,
        string valueColumn,
        CONSTANTS.AggregationType aggregation)
    {
        var groupedData = records.GroupBy(r => r[categoryColumn]);
        var result = new List<(string, double)>();

        foreach (var group in groupedData)
        {
            var numericValues = group.Select(r =>
            {
                if (TryParseNumericValue(r[valueColumn], out double val))
                    return val;
                return (double?)null;
            }).Where(val => val.HasValue).Select(val => val!.Value).ToList();

            if (numericValues.Count == 0)
            {
                continue;
            }

            double aggregatedValue = aggregation switch
            {
                CONSTANTS.AggregationType.Total => numericValues.Sum(),
                CONSTANTS.AggregationType.Average => numericValues.Average(),
                CONSTANTS.AggregationType.Minimum => numericValues.Min(),
                CONSTANTS.AggregationType.Maximum => numericValues.Max(),
                _ => 0
            };

            result.Add((group.Key, aggregatedValue));
        }

        return result;
    }

    private static bool TryParseNumericValue(
        string value,
        out double result)
    {
        value = value.Replace(' ', '\0');

        string cleanedValue = new(value
            .Where(c => Char.IsDigit(c) || c == '.' || c == '-' || c == ',')
            .ToArray());

        return double.TryParse(cleanedValue.Replace(',', '.').Replace('-', '.'), out result);
    }
}
