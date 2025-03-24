using MongoDB.Driver;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OPTIONS = DataWise.Common.Options;
using CONSTANTS = DataWise.Common.Constants;
using RELATIONAL = DataWise.Data.DbContexts.Relational;
using N_RELATIONAL = DataWise.Data.DbContexts.NonRelational;
using R_MODELS = DataWise.Data.DbContexts.Relational.Models;
using NR_MODELS = DataWise.Data.DbContexts.NonRelational.Models;
using R_REPOSITORIES = DataWise.Data.Repositories.Relational;
using NR_REPOSITORIES = DataWise.Data.Repositories.NonRelational;

namespace DataWise.Api.Extensions;

/// <summary>
/// Provides extension methods to register application services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers configuration settings from the configuration files.
    /// </summary>
    public static IServiceCollection AddConfigurations(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<OPTIONS.UserDbSettings>(
            configuration.GetSection(CONSTANTS.GeneralConstants.UserDbSettingsName));
        services.Configure<OPTIONS.KnowledgeNexusDbSettings>(
            configuration.GetSection(CONSTANTS.GeneralConstants.MongoDbSettingsName));

        return services;
    }

    /// <summary>
    /// Registers MongoDB-related services including the database, context, and repositories.
    /// </summary>
    public static IServiceCollection AddMongoServices(
        this IServiceCollection services)
    {
        services.AddSingleton<IMongoDatabase>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<OPTIONS.KnowledgeNexusDbSettings>>().Value;
            var client = new MongoClient(settings.ConnectionString);

            return client.GetDatabase(settings.DatabaseName);
        });

        services.AddSingleton<N_RELATIONAL.KnowledgeNexusDbContext>();

        services.AddScoped<NR_REPOSITORIES.IMongoRepository<NR_MODELS.DataStructure, string>>(sp =>
        {
            var database = sp.GetRequiredService<IMongoDatabase>();

            return new NR_REPOSITORIES.MongoRepository<NR_MODELS.DataStructure, string>(
                database, CONSTANTS.GeneralConstants.DataStructureCollectionName);
        });

        services.AddScoped<NR_REPOSITORIES.IMongoRepository<NR_MODELS.Algorithm, string>>(sp =>
        {
            var database = sp.GetRequiredService<IMongoDatabase>();

            return new NR_REPOSITORIES.MongoRepository<NR_MODELS.Algorithm, string>(
                database, CONSTANTS.GeneralConstants.AlgorithmCollectionName);
        });

        return services;
    }

    /// <summary>
    /// Registers SQL-related services including the database, context, and repositories.
    /// </summary>
    public static IServiceCollection AddSQLServices(
        this IServiceCollection services)
    {
        services.AddScoped<R_REPOSITORIES.ISQLRepository<R_MODELS.Question, string>,
                    R_REPOSITORIES.SQLRepository<R_MODELS.Question, string>>();

        services.AddScoped<R_REPOSITORIES.ISQLRepository<R_MODELS.ChatSession, string>,
            R_REPOSITORIES.SQLRepository<R_MODELS.ChatSession, string>>();

        services.AddScoped<R_REPOSITORIES.ISQLRepository<R_MODELS.ChatMessage, string>,
            R_REPOSITORIES.SQLRepository<R_MODELS.ChatMessage, string>>();

        services.AddDbContext<RELATIONAL.InterviewDbContext>((sp, options) =>
        {
            var settings = sp.GetRequiredService<IOptions<OPTIONS.UserDbSettings>>().Value;
            options.UseSqlServer(settings.ConnectionString);
        });

        return services;
    }

    /// <summary>
    /// Registers Swagger/OpenAPI services.
    /// </summary>
    public static IServiceCollection AddCustomSwagger(
        this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "DataWise API", Version = "v1" });

            options.UseInlineDefinitionsForEnums();
        });

        return services;
    }

    /// <summary>
    /// Registers the application data seeders.
    /// </summary>
    public static IServiceCollection AddDataSeeders(
        this IServiceCollection services)
    {
        services.AddTransient<N_RELATIONAL.DataSeeder>();

        services.AddTransient<RELATIONAL.DataSeeder>();

        return services;
    }
}