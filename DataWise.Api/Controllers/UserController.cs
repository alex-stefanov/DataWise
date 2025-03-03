using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DataWise.Data.DbContexts.Releational.Models;
using DataWise.Common.DTOs;

namespace DataWise.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(
    UserManager<WiseClient> userManager,
    SignInManager<WiseClient> signInManager)
    : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody]
        RegisterDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new WiseClient
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Points = 0
        };

        IdentityResult result;
        try
        {
            result = await userManager.CreateAsync(user, model.Password);
        }
        catch(Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }

        if (result.Succeeded)
        {
            await signInManager.SignInAsync(user, isPersistent: false);
            return Ok(new { message = "User registered successfully." });
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody]
        LoginDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            return Ok(new { message = "User logged in successfully." });
        }

        return Unauthorized(new { message = "Invalid login attempt." });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Ok(new { message = "User logged out successfully." });
    }

    [HttpGet("profile")]
    public async Task<IActionResult> Profile()
    {
        var userId = userManager.GetUserId(User);
        if (userId is null)
            return Unauthorized();

        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
            return NotFound();

        return Ok(new
        {
            user.Email,
            user.FirstName,
            user.LastName,
            user.Points
        });
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile(
        [FromBody] 
        UpdateProfileDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await userManager.FindByEmailAsync(model.Email);
        if (user is null)
            return NotFound();

        if(!string.IsNullOrEmpty(model.FirstName))
            model.FirstName = user.FirstName;

        if (!string.IsNullOrEmpty(model.LastName))
            user.FirstName = model.LastName;

        var result = await userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return Ok(new { message = "Profile updated successfully." });
        }

        return BadRequest(result.Errors);
    }
}