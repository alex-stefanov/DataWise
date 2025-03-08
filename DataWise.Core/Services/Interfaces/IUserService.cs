using System.Security.Claims;
using DataWise.Common.DTOs;
using DataWise.Data.DbContexts.Releational.Models;

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
    Task<(bool Succeeded, string Message, IEnumerable<string>? Errors)> RegisterAsync(
        RegisterDto model);

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="model">The login details.</param>
    /// <returns>
    /// A tuple containing a flag indicating success and a message.
    /// </returns>
    Task<(bool Succeeded, string Message)> LoginAsync(
        LoginDto model);

    /// <summary>
    /// Logs out the current user.
    /// </summary>
    Task LogoutAsync();

    /// <summary>
    /// Retrieves the current user's profile.
    /// </summary>
    /// <param name="user">The current user's claims principal.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    Task<WiseClient?> GetProfileAsync(
        ClaimsPrincipal user);

    /// <summary>
    /// Updates the current user's profile.
    /// </summary>
    /// <param name="model">The updated profile details.</param>
    /// <returns>
    /// A tuple containing a flag indicating success, a message, and any error messages if present.
    /// </returns>
    Task<(bool Succeeded, string Message, IEnumerable<string>? Errors)> UpdateProfileAsync(
        UpdateProfileDto model);
}