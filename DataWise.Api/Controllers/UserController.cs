using Microsoft.AspNetCore.Mvc;
using DTOS = DataWise.Common.DTOs;
using INTERAFCES = DataWise.Core.Services.Interfaces;

namespace DataWise.Api.Controllers;

/// <summary>
/// Controller responsible for user authentication and profile management.
/// </summary>
/// <param name="userService">The user service instance.</param>
[Route("api/user")]
[ApiController]
public class UserController (
    INTERAFCES.IUserService userService)
    : ControllerBase
{
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="model">The registration details.</param>
    /// <returns>An action result indicating the outcome of the registration.</returns>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromBody]
        DTOS.RegisterDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var (succeeded, message, errors) = await userService
            .RegisterAsync(model);

        if (succeeded)
            return Ok(new { message });

        return BadRequest(new { message, errors });
    }

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="model">The login details.</param>
    /// <returns>An action result indicating the outcome of the login attempt.</returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromBody]
        DTOS.LoginDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var (succeeded, message) = await userService
            .LoginAsync(model);

        if (succeeded)
            return Ok(new { message });

        return Unauthorized(new { message });
    }

    /// <summary>
    /// Logs out the current user.
    /// </summary>
    /// <returns>An action result indicating the outcome of the logout operation.</returns>
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout()
    {
        await userService
            .LogoutAsync();

        return Ok(new { message = "User logged out successfully." });
    }

    /// <summary>
    /// Retrieves the current user's profile.
    /// </summary>
    /// <returns>The user's profile details.</returns>
    [HttpGet("profile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Profile()
    {
        var user = await userService
            .GetProfileAsync(User);

        if (user is null)
            return Unauthorized(new { message = "User is not authenticated." });

        return Ok(new
        {
            user.Email,
            user.FirstName,
            user.LastName,
            user.Points
        });
    }

    /// <summary>
    /// Updates the current user's profile.
    /// </summary>
    /// <param name="model">The updated profile details.</param>
    /// <returns>An action result indicating the outcome of the update operation.</returns>
    [HttpPut("profile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProfile(
        [FromBody] 
        DTOS.UpdateProfileDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var (succeeded, message, errors) = await userService.UpdateProfileAsync(model);

        if (succeeded)
            return Ok(new { message });
        else if (message.Contains("not found"))
            return NotFound(new { message });

        return BadRequest(new { message, errors });
    }
}