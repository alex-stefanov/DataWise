using DataWise.Common;
using DataWise.Data.DbContexts.NonReleational;
using DataWise.Data.DbContexts.NonReleational.Models;
using DataWise.Data.Repositories.NonReleational;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataWise.Api;

public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                policyBuilder =>
                {
                    policyBuilder.AllowAnyOrigin()
                                 .AllowAnyMethod()
                                 .AllowAnyHeader();
                });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.Configure<DataStructuresSettings>(
            builder.Configuration.GetSection("MongoDbSettings"));

        builder.Services.AddSingleton<IMongoDatabase>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<DataStructuresSettings>>().Value;
            var client = new MongoClient(settings.ConnectionString);
            return client.GetDatabase(settings.DatabaseName);
        });

        builder.Services.AddSingleton<DataStructuresDbContext>();

        builder.Services.AddScoped<IMongoRepository<DataStructure, string>>(sp =>
        {
            var database = sp.GetRequiredService<IMongoDatabase>();
            return new MongoRepository<DataStructure, string>(database, "DataStructures");
        });

        builder.Services.AddTransient<DataSeeder>();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
            await seeder.SeedAsync();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler("/error");

        app.UseHttpsRedirection();
        app.UseCors("AllowAll");
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}