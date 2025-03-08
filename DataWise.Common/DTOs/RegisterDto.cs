namespace DataWise.Common.DTOs;

/// <summary>
/// Represents the data transfer object (DTO) for user registration.
/// </summary>
public class RegisterDto
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
    /// Gets or sets the user's first name.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public required string LastName { get; set; }
}