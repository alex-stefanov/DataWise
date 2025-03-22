using DotNetEnv;
using DataWise.Api.Extensions;
using CONSTANTS = DataWise.Common.Constants;

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
            .AddCustomConfiguration(
                builder.Configuration)
            .AddMongoServices()
            .AddDataSeeder();

        builder.AddUserServices();

        var app = builder.Build();

        await app.ApplyMigrationsAndSeedAsync();

        app
            .UseCustomSwagger()
            .UseCustomExceptionHandler()
            .UseCustomHttpsRedirection()
            .UseCustomCors();

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}