namespace DataWise.Common.DTOs;

/// <summary>
/// Represents the form data required to start a new interview session.
/// </summary>
public class StartSessionDto
{
    /// <summary>
    /// Gets or sets the identifier of the user starting the session.
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Gets or sets the question category.
    /// </summary>
    public required string Category { get; set; }

    /// <summary>
    /// Gets or sets the difficulty level of the question.
    /// </summary>
    public required string Difficulty { get; set; }
}
