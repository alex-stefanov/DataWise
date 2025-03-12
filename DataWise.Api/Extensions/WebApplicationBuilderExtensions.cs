using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenAI_API;
using OPTIONS = DataWise.Common.Options;
using CONSTANTS = DataWise.Common.Constants;
using INTERFACES = DataWise.Core.Services.Interfaces;
using IMPLEMENTATIONS = DataWise.Core.Services.Implementations;
using RELEATIONAL = DataWise.Data.DbContexts.Releational;
using R_MODELS = DataWise.Data.DbContexts.Releational.Models;

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
    /// Registers SQL Server context and ASP.NET Core Identity services.
    /// </summary>
    public static IServiceCollection AddUserServices(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<OpenAIAPI>(sp =>
        {
            var apiKey = Environment.GetEnvironmentVariable(CONSTANTS.GeneralConstants.OpenAIApiEnvKey) 
                ?? throw new Exception("Missing OPENAI_API configuration.");
            return new OpenAIAPI(new APIAuthentication(apiKey));
        });

        builder.Services.AddDbContext<RELEATIONAL.UserDbContext>((sp, options) =>
        {
            var settings = sp.GetRequiredService<IOptions<OPTIONS.UserDbSettings>>().Value;
            options.UseSqlServer(settings.ConnectionString);
        });

        builder.Services.AddIdentity<R_MODELS.WiseClient, IdentityRole<string>>(cfg =>
        {
            builder.ConfigureIdentity(cfg);
        })
        .AddEntityFrameworkStores<RELEATIONAL.UserDbContext>()
        .AddRoles<IdentityRole<string>>()
        .AddSignInManager<SignInManager<R_MODELS.WiseClient>>()
        .AddUserManager<UserManager<R_MODELS.WiseClient>>();

        builder.Services.AddScoped<INTERFACES.IAlgorithmService, IMPLEMENTATIONS.AlgorithmService>();
        builder.Services.AddScoped<INTERFACES.IChartService, IMPLEMENTATIONS.ChartService>();
        builder.Services.AddScoped<INTERFACES.IStructureService, IMPLEMENTATIONS.StructureService>();
        builder.Services.AddScoped<INTERFACES.IUserService, IMPLEMENTATIONS.UserService>();

        return builder.Services;
    }
}