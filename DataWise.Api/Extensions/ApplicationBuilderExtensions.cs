﻿using CONSTANTS = DataWise.Common.Constants;

namespace DataWise.Api.Extensions;

/// <summary>
/// Provides extension methods for configuring the application pipeline.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Configures the application to use Swagger in development.
    /// </summary>
    public static IApplicationBuilder UseCustomSwagger(
        this IApplicationBuilder app)
    {
        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }

    /// <summary>
    /// Configures the application to use a custom exception handler.
    /// </summary>
    public static IApplicationBuilder UseCustomExceptionHandler(
        this IApplicationBuilder app)
    {
        app.UseExceptionHandler("/error");

        return app;
    }

    /// <summary>
    /// Configures the application to use HTTPS redirection.
    /// </summary>
    public static IApplicationBuilder UseCustomHttpsRedirection(
        this IApplicationBuilder app)
    {
        app.UseHttpsRedirection();

        return app;
    }

    /// <summary>
    /// Configures the application to use the CORS policy named "AllowAll".
    /// </summary>
    public static IApplicationBuilder UseCustomCors(
        this IApplicationBuilder app)
    {
        app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://datawise.techlab.cloud", "http://localhost:4200"));

        return app;
    }
}
