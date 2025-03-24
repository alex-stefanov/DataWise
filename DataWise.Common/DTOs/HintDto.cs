namespace DataWise.Common.DTOs;

/// <summary>
/// Represents a data transfer object (DTO) for requesting a hint within a session.
/// </summary>
public class HintDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the session in which the hint is requested.
    /// </summary>
    public required string SessionId { get; set; }
}

