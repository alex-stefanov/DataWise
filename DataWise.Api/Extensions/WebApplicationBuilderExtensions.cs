using Microsoft.AspNetCore.Identity;
using OpenAI_API;
using CONSTANTS = DataWise.Common.Constants;
using INTERFACES = DataWise.Core.Services.Interfaces;
using IMPLEMENTATIONS = DataWise.Core.Services.Implementations;
using RELATIONAL = DataWise.Data.DbContexts.Relational;
using R_MODELS = DataWise.Data.DbContexts.Relational.Models;

namespace DataWise.Api.Extensions;

/// <summary>
/// Provides extension methods for configuring Identity, database contexts, and dependency injection services.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Configures Identity options using values from the application's configuration.
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder instance.</param>
    /// <param name="cfg">The IdentityOptions instance to configure.</param>
    public static void ConfigureIdentity(
        this WebApplicationBuilder builder,
        IdentityOptions cfg)
    {
        cfg.Password.RequireDigit =
            builder.Configuration.GetValue<bool>("Identity:Password:RequireDigits");
        cfg.Password.RequireLowercase =
            builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
        cfg.Password.RequireUppercase =
            builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
        cfg.Password.RequireNonAlphanumeric =
            builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumerical");
        cfg.Password.RequiredLength =
            builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
        cfg.Password.RequiredUniqueChars =
            builder.Configuration.GetValue<int>("Identity:Password:RequiredUniqueCharacters");

        cfg.SignIn.RequireConfirmedAccount =
            builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
        cfg.SignIn.RequireConfirmedEmail =
            builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");
        cfg.SignIn.RequireConfirmedPhoneNumber =
            builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");

        cfg.User.RequireUniqueEmail =
            builder.Configuration.GetValue<bool>("Identity:User:RequireUniqueEmail");
    }

    /// <summary>
    /// Registers custom services.
    /// </summary>
    public static WebApplicationBuilder AddUserServices(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<INTERFACES.IAlgorithmService, IMPLEMENTATIONS.AlgorithmService>();
        builder.Services.AddScoped<INTERFACES.IChartService, IMPLEMENTATIONS.ChartService>();
        builder.Services.AddScoped<INTERFACES.IStructureService, IMPLEMENTATIONS.StructureService>();
        builder.Services.AddScoped<INTERFACES.IUserService, IMPLEMENTATIONS.UserService>();
        builder.Services.AddScoped<INTERFACES.IInterviewService, IMPLEMENTATIONS.InterviewService>();

        return builder;
    }

    /// <summary>
    /// Configures custom identity.
    /// </summary>
    public static WebApplicationBuilder AddIdentityAndRoles(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<R_MODELS.WiseClient, IdentityRole<string>>(cfg =>
        {
            builder.ConfigureIdentity(cfg);
        })
        .AddEntityFrameworkStores<RELATIONAL.InterviewDbContext>()
        .AddRoles<IdentityRole<string>>()
        .AddSignInManager<SignInManager<R_MODELS.WiseClient>>()
        .AddUserManager<UserManager<R_MODELS.WiseClient>>();

        return builder;
    }

    /// <summary>
    /// Adds OpenAI API services.
    /// </summary>
    public static WebApplicationBuilder AddOpenAI(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<OpenAIAPI>(sp =>
        {
            var apiKey = Environment.GetEnvironmentVariable(CONSTANTS.GeneralConstants.OpenAIApiEnvKey)
                ?? throw new Exception("Missing OPENAI_API configuration.");
            return new OpenAIAPI(new APIAuthentication(apiKey));
        });

        return builder;
    }
}