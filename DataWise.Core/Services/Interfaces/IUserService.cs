﻿using DTOS = DataWise.Common.DTOs;
using MODELS = DataWise.Data.DbContexts.Relational.Models;

namespace DataWise.Core.Services.Interfaces;

/// <summary>
/// Provides authentication and profile management functionalities for users.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="model">The registration details.</param>
    /// <returns>
    /// A tuple containing a flag indicating success, a message, and any error messages if present.
    /// </returns>
    Task<(bool Succeeded, string UserId, string Message, IEnumerable<string>? Errors)> RegisterAsync(
        DTOS.RegisterDto model);

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="model">The login details.</param>
    /// <returns>
    /// A tuple containing a flag indicating success and a message.
    /// </returns>
    Task<(bool Succeeded, string UserId, string Message)> LoginAsync(
       DTOS.LoginDto model);

    /// <summary>
    /// Logs out the current user.
    /// </summary>
    Task LogoutAsync();

    /// <summary>
    /// Retrieves the current user's profile.
    /// </summary>
    /// <param name="userId">The current user's id.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    Task<MODELS.WiseClient?> GetProfileAsync(
        string userId);

    /// <summary>
    /// Updates the current user's profile.
    /// </summary>
    /// <param name="model">The updated profile details.</param>
    /// <returns>
    /// A tuple containing a flag indicating success, a message, and any error messages if present.
    /// </returns>
    Task<(bool Succeeded, string Message, IEnumerable<string>? Errors)> UpdateProfileAsync(
        DTOS.UpdateProfileDto model);
}