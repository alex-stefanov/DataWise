namespace DataWise.Common.DTOs;

/// <summary>
/// Represents the data transfer object (DTO) for user login.
/// </summary>
public class LoginDto
{
    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the user's password.
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user should remain logged in across sessions.
    /// </summary>
    public bool RememberMe { get; set; }
}