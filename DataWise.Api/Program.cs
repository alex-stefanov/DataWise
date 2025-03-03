using MongoDB.Driver;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using DataWise.Api.Extensions;
using DataWise.Data.DbContexts.NonReleational;
using DataWise.Data.DbContexts.NonReleational.Models;
using DataWise.Data.DbContexts.Releational;
using DataWise.Data.DbContexts.Releational.Models;
using DataWise.Data.Repositories.NonReleational;
using DataWise.Common.Options;

namespace DataWise.Api;

public class Program
{
    public async static Task Main(
        string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<UserDbSettings>(
            builder.Configuration.GetSection("UserDbSettings"));

        builder.Services.Configure<KnowledgeNexusDbSettings>(
            builder.Configuration.GetSection("MongoDbSettings"));

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

        builder.Services.AddSingleton<IMongoDatabase>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<KnowledgeNexusDbSettings>>().Value;
            var client = new MongoClient(settings.ConnectionString);
            return client.GetDatabase(settings.DatabaseName);
        });

        builder.Services.AddSingleton<KnowledgeNexusDbContext>();

        builder.Services.AddScoped<IMongoRepository<DataStructure, string>>(sp =>
        {
            var database = sp.GetRequiredService<IMongoDatabase>();
            return new MongoRepository<DataStructure, string>(database, "DataStructures");
        });

        builder.Services.AddScoped<IMongoRepository<Algorithm, string>>(sp =>
        {
            var database = sp.GetRequiredService<IMongoDatabase>();
            return new MongoRepository<Algorithm, string>(database, "Algorithms");
        });

        builder.Services.AddTransient<DataSeeder>();

        builder.Services
            .AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer("Server=localhost;Database=DataWiseClient;User Id=SA;Password=Str0ngPa$$w0rd;TrustServerCertificate=True;");
            });

        builder.Services
           .AddIdentity<WiseClient, IdentityRole<string>>(cfg =>
           {
               builder.ConfigureIdentity(cfg);
           })
           .AddEntityFrameworkStores<UserDbContext>()
           .AddRoles<IdentityRole<string>>()
           .AddSignInManager<SignInManager<WiseClient>>()
           .AddUserManager<UserManager<WiseClient>>();

        var app = builder.Build();


        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<UserDbContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while creating the database.");
            }
        }

        using (var scope = app.Services.CreateScope())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
            await seeder.SeedAllAsync();
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