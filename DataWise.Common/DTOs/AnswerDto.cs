namespace DataWise.Common.DTOs;

/// <summary>
/// Represents a data transfer object (DTO) for user answers within a session.
/// </summary>
public class AnswerDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the session.
    /// </summary>
    public required string SessionId { get; set; }

    /// <summary>
    /// Gets or sets the user's answer.
    /// </summary>
    public required string UserAnswer { get; set; }
}
