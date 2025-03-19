using CsvHelper.Configuration;
using MODELS = DataWise.Data.DbContexts.Releational.Models;

namespace DataWise.Data.DbContexts.Releational;

/// <summary>
/// Maps CSV columns to the Question properties.
/// </summary>
public sealed class QuestionCsvMap
    : ClassMap<MODELS.Question>
{
    public QuestionCsvMap()
    {
        Map(m => m.Category).Name("Category");
        Map(m => m.QuestionText).Name("Question");
        Map(m => m.AnswerText).Name("Answer");
        Map(m => m.Difficulty).Name("Difficulty");
    }
}