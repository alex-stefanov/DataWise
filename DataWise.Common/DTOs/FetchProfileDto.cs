namespace DataWise.Common.DTOs;

/// <summary>
/// Represents a data transfer object (DTO) for fetching a user's profile.
/// </summary>
public class FetchProfileDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the user whose profile is being fetched.
    /// </summary>
    public required string UserId { get; set; }
}
