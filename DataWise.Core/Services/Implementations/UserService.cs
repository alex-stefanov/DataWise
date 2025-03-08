using System.Security.Claims;
using DataWise.Common.DTOs;
using DataWise.Core.Services.Interfaces;
using DataWise.Data.DbContexts.Releational.Models;
using Microsoft.AspNetCore.Identity;

namespace DataWise.Core.Services.Implementations;

/// <summary>
/// Implements user-related operations using ASP.NET Core Identity.
/// </summary>
/// <param name="userManager">The user manager instance.</param>
/// <param name="signInManager">The sign in manager instance.</param>
public class UserService(
    UserManager<WiseClient> userManager,
    SignInManager<WiseClient> signInManager)
    : IUserService
{
    /// <inheritdoc />
    public async Task<(bool Succeeded, string Message, IEnumerable<string>? Errors)> RegisterAsync(
        RegisterDto model)
    {
        var user = new WiseClient
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Points = 0
        };

        try
        {
            var result = await userManager
                .CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await signInManager
                    .SignInAsync(user, isPersistent: false);

                return (true, "User registered successfully.", null);
            }

            return (false, "User registration failed.", result.Errors?.Select(e => e.Description));
        }
        catch (Exception ex)
        {
            return (false, $"An error occurred: {ex.Message}", null);
        }
    }

    /// <inheritdoc />
    public async Task<(bool Succeeded, string Message)> LoginAsync(
        LoginDto model)
    {
        var result = await signInManager
            .PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
            return (true, "User logged in successfully.");

        return (false, "Invalid login attempt.");
    }

    /// <inheritdoc />
    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }

    /// <inheritdoc />
    public async Task<WiseClient?> GetProfileAsync(
        ClaimsPrincipal user)
    {
        var userId = userManager
            .GetUserId(user);

        if (userId is null)
            return null;

        return await userManager.FindByIdAsync(userId);
    }

    /// <inheritdoc />
    public async Task<(bool Succeeded, string Message, IEnumerable<string>? Errors)> UpdateProfileAsync(
        UpdateProfileDto model)
    {
        var user = await userManager
            .FindByEmailAsync(model.Email);

        if (user is null)
            return (false, "User not found.", null);

        if (!string.IsNullOrEmpty(model.FirstName))
            user.FirstName = model.FirstName;

        if (!string.IsNullOrEmpty(model.LastName))
            user.LastName = model.LastName;

        var result = await userManager
            .UpdateAsync(user);

        if (result.Succeeded)
            return (true, "Profile updated successfully.", null);

        return (false, "Profile update failed.", result.Errors?.Select(e => e.Description));
    }
}