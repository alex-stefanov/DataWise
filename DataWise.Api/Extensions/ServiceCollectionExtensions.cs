using MongoDB.Driver;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using OPTIONS = DataWise.Common.Options;
using CONSTANTS = DataWise.Common.Constants;
using RELEATIONAL = DataWise.Data.DbContexts.Releational;
using N_RELEATIONAL = DataWise.Data.DbContexts.NonReleational;
using R_MODELS = DataWise.Data.DbContexts.Releational.Models;
using NR_MODELS = DataWise.Data.DbContexts.NonReleational.Models;
using R_REPOSITORIES = DataWise.Data.Repositories.Releational;
using NR_REPOSITORIES = DataWise.Data.Repositories.NonReleational;

namespace DataWise.Api.Extensions;

//TODO: Better services organisation

/// <summary>
/// Provides extension methods to register application services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers configuration settings from the configuration files.
    /// </summary>
    public static IServiceCollection AddCustomConfiguration(
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

        services.AddSingleton<N_RELEATIONAL.KnowledgeNexusDbContext>();

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

        services.AddScoped<R_REPOSITORIES.ISQLRepository<R_MODELS.Question, string>,
            R_REPOSITORIES.SQLRepository<R_MODELS.Question, string>>();

        services.AddScoped<R_REPOSITORIES.ISQLRepository<R_MODELS.ChatSession, string>,
            R_REPOSITORIES.SQLRepository<R_MODELS.ChatSession, string>>();

        services.AddScoped<R_REPOSITORIES.ISQLRepository<R_MODELS.ChatMessage, string>,
            R_REPOSITORIES.SQLRepository<R_MODELS.ChatMessage, string>>();

        return services;
    }

    /// <summary>
    /// Registers a CORS policy named "AllowAll".
    /// </summary>
    public static IServiceCollection AddCustomCors(
        this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(CONSTANTS.GeneralConstants.PolicyValue, policyBuilder =>
            {
                policyBuilder.AllowAnyOrigin()
                             .AllowAnyMethod()
                             .AllowAnyHeader();
            });
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
    /// Registers the application data seeder.
    /// </summary>
    public static IServiceCollection AddDataSeeder(
        this IServiceCollection services)
    {
        services.AddTransient<N_RELEATIONAL.DataSeeder>();

        services.AddTransient<RELEATIONAL.DataSeeder>();

        return services;
    }
}