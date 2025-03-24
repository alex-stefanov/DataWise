using System.Globalization;
using CsvHelper;
using REPOSITORIES = DataWise.Data.Repositories.Relational;
using MODELS = DataWise.Data.DbContexts.Relational.Models;

namespace DataWise.Data.DbContexts.Relational;

/// <summary>
/// A simple seeder class that imports questions from a CSV using a repository.
/// </summary>
public class DataSeeder(
    REPOSITORIES.ISQLRepository<MODELS.Question, string> questionRepository)
{
    /// <summary>
    /// Reads the CSV file and inserts questions if none exist in the repository.
    /// Assumes the CSV header contains keys: ID,Category,Question,Answer,Difficulty.
    /// Uses CsvHelper for robust CSV parsing.
    /// </summary>
    /// <param name="csvFilePath">Path to the CSV file containing question data.</param>
    public async Task SeedQuestionsAsync(
        string csvFilePath)
    {
        var existingQuestions = await questionRepository.GetAllAsync();
        if (existingQuestions.Any())
            return;

        using var reader = new StreamReader(csvFilePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Context.RegisterClassMap<QuestionCsvMap>();

        var questions = csv.GetRecords<MODELS.Question>().ToList();

        if (questions.Count != 0)
            await questionRepository.AddRangeAsync([.. questions]);
    }
}