namespace DataWise.Common.DTOs;

/// <summary>
/// Represents the data transfer object (DTO) for updating a user's profile.
/// </summary>
public class UpdateProfileDto
{
    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the user's first name. This field is optional.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the user's last name. This field is optional.
    /// </summary>
    public string? LastName { get; set; }
}