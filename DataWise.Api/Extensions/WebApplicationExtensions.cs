using Microsoft.EntityFrameworkCore;
using DataWise.Data.DbContexts.Releational;

namespace DataWise.Api.Extensions;

/// <summary>
/// Provides extension methods for configuring and initializing the web application.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Applies pending EF Core migrations and seeds initial data.
    /// </summary>
    public static async Task ApplyMigrationsAndSeedAsync(
        this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<InterviewDbContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating the database.");
        }

        using var seedScope = app.Services.CreateScope();
        var seeder = seedScope.ServiceProvider.GetRequiredService<Data.DbContexts.NonReleational.DataSeeder>();

        await seeder.SeedAllAsync();

        var seeder1 = seedScope.ServiceProvider.GetRequiredService<Data.DbContexts.Releational.DataSeeder>();

        string projectRoot = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName
                             ?? throw new Exception("Project root not found");

        string filePath = Path.Combine(projectRoot, "DataWise.Data", "DbContexts", "Releational", "Data", "interview_questions.csv");

        Console.WriteLine($"Loading file from: {filePath}");

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return; 
        }

        await seeder1.SeedQuestionsAsync(filePath);
    }
}
