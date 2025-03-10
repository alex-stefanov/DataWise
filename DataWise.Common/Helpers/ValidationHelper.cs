using System.Globalization;
using System.Text.RegularExpressions;
using CONSTANTS = DataWise.Common.Constants;
using OpenAI_API;

namespace DataWise.Common.Helpers;

/// <summary>
/// Provides helper methods for processing, aggregating and validating data.
/// </summary>
public static class ValidationHelper
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
                if (TryParseGenericNumericValue(r[valueColumn], out double val))
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

    /// <summary>
    /// Checks if the specified column should be treated as numeric data.
    /// It uses the first non-empty sample value from the records and asks OpenAI for validation.
    /// </summary>
    /// <param name="records">A list of record dictionaries.</param>
    /// <param name="columnName">The name of the column to check.</param>
    /// <param name="openAIAPI">An instance of the OpenAI API client.</param>
    /// <returns>True if the column is validated as numeric; otherwise false.</returns>
    public static async Task<bool> IsColumnNumericAsync(
        List<Dictionary<string, string>> records,
        string columnName,
        OpenAIAPI openAIAPI)
    {
        if (records is null 
            || records.Count == 0)
            throw new ArgumentException("Records cannot be null or empty.");

        string? sampleValue = records
            .Select(r => r.ContainsKey(columnName) ? r[columnName] : null)
            .FirstOrDefault(val => !string.IsNullOrWhiteSpace(val));

        if (string.IsNullOrWhiteSpace(sampleValue))
            return false;

        return await ValidateNumericColumnAsync(
            columnName,
            sampleValue,
            openAIAPI);
    }

    /// <summary>
    /// Uses OpenAI to validate if the column (based on its name and a sample value)
    /// should be treated as numeric.
    /// A system message is added to enforce that the response is strictly "Yes" or "No".
    /// </summary>
    /// <param name="columnName">The name of the column.</param>
    /// <param name="sampleValue">A sample value from the column.</param>
    /// <param name="openAIAPI">An instance of the OpenAI API client.</param>
    /// <returns>True if OpenAI indicates the column is numeric; otherwise false.</returns>
    private static async Task<bool> ValidateNumericColumnAsync(
         string columnName,
         string sampleValue,
         OpenAIAPI openAIAPI)
    {
        var conversation = openAIAPI.Chat.CreateConversation();
        conversation.AppendSystemMessage(@"You are a decision engine that determines whether a given column should be treated as numeric.
            The sample values may include additional non-numeric characters such as currency symbols, measurement units, or other text.
            However, if the value contains any numeric component that can be aggregated in a chart or diagram, then the column should be considered numeric.
            Use the column name as context and evaluate the numeric portion within the value. Please answer with 'Yes' or 'No' only.");

        string prompt = @$"Based on the column name '{columnName}' and the sample value '{sampleValue}',
            should this column be treated as numeric data?";

        conversation.AppendUserInput(prompt);

        var response = await conversation.GetResponseFromChatbotAsync();
        return response.Trim().StartsWith("yes", StringComparison.CurrentCultureIgnoreCase);
    }

    /// <summary>
    /// Attempts to parse a generic numeric value from a string.
    /// This method extracts the first numeric pattern it finds, handling cases
    /// like '$ 743', 'BGN 88', or '23MB'.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="result">The parsed numeric value if successful.</param>
    /// <returns>True if a numeric value could be extracted and parsed; otherwise false.</returns>
    private static bool TryParseGenericNumericValue(
        string value,
        out double result)
    {
        result = 0;

        if (string.IsNullOrWhiteSpace(value))
            return false;

        var regexPattern = @"[-+]?(?:(?:\d{1,3}(?:[ ,:\.-]\d{3})+)|\d+)(?:[.,]\d+)?";

        var match = Regex.Match(value, regexPattern);

        if (match.Success)
        {
            string numericString = match.Value;

            numericString = Regex.Replace(numericString, @"[ ,:\.-](?=\d{3}(?:[ ,:\.-]|$))", "");

            numericString = numericString.Replace(',', '.');

            return double.TryParse(numericString, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
        }

        return false;
    }
}