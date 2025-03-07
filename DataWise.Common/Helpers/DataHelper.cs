using DataWise.Common.Constants;

namespace DataWise.Common.Helpers;

public static class DataHelper
{
    /// <summary>
    /// Groups records by the category column and applies the specified aggregation on the numeric value.
    /// Returns a list of tuples (Category, AggregatedValue).
    /// </summary>
    public static List<(string Category, double AggregatedValue)> ProcessDataAggregation(
        List<Dictionary<string, string>> records,
        string categoryColumn,
        string valueColumn,
        AggregationType aggregation)
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
                AggregationType.Total => numericValues.Sum(),
                AggregationType.Average => numericValues.Average(),
                AggregationType.Minimum => numericValues.Min(),
                AggregationType.Maximum => numericValues.Max(),
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
