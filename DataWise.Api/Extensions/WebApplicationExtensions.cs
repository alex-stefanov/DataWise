using Microsoft.EntityFrameworkCore;
using N_RELATIONAL = DataWise.Data.DbContexts.NonRelational;
using RELATIONAL = DataWise.Data.DbContexts.Relational;

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
            var context = services.GetRequiredService<RELATIONAL.InterviewDbContext>();
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating the database.");
        }

        using var seedScope = app.Services.CreateScope();
        var mongoSeeder = seedScope.ServiceProvider.GetRequiredService<N_RELATIONAL.DataSeeder>();

        await mongoSeeder.SeedAllAsync();

        var sqlSeeder = seedScope.ServiceProvider.GetRequiredService<RELATIONAL.DataSeeder>();

        string filePath;

        if (app.Environment.IsDevelopment())
        {
            string projectRoot = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName
                ?? throw new Exception("Project root not found");

            filePath = Path.Combine(projectRoot, "DataWise.Data", "DbContexts", "Relational", "Data", "interview_questions.csv");
        }
        else
        {
            filePath = Path.Combine("/app/data/interview_questions.csv");
        }

        await sqlSeeder.SeedQuestionsAsync(filePath);
    }
}