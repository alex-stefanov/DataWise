﻿using Microsoft.EntityFrameworkCore;
using DataWise.Data.DbContexts.NonReleational;
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
            var context = services.GetRequiredService<UserDbContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating the database.");
        }

        using var seedScope = app.Services.CreateScope();
        var seeder = seedScope.ServiceProvider.GetRequiredService<DataSeeder>();

        await seeder.SeedAllAsync();
    }
}
