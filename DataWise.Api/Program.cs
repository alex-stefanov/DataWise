using DotNetEnv;
using DataWise.Api.Extensions;

namespace DataWise.Api;

public class Program
{
    public async static Task Main(
        string[] args)
    {
        Env.Load();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddCors()
            .AddCustomSwagger()
            .AddControllers();

        builder.Services
            .AddConfigurations(
                builder.Configuration)
            .AddMongoServices()
            .AddSQLServices()
            .AddDataSeeders();

        builder
            .AddOpenAI()
            .AddIdentityAndRoles()
            .AddUserServices();

        var app = builder.Build();

        await app
            .ApplyMigrationsAndSeedAsync();

        app
            .UseCustomSwagger()
            .UseExceptionHandler("/error")
            .UseHttpsRedirection()
            .UseCustomCors();

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}