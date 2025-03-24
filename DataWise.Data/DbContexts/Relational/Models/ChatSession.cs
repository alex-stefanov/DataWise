using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataWise.Data.DbContexts.Relational.Models;

/// <summary>
/// Represents a single Q&A session for one question.
/// </summary>
public class ChatSession
{
    /// <summary>
    /// Primary key as a string. Defaults to a GUID if not explicitly set.
    /// </summary>
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The user's ID (from Identity) who started the session.
    /// </summary>
    [Required]
    public string UserId { get; set; } = null!;

    /// <summary>
    /// The category the user selected for this session.
    /// </summary>
    [Required]
    public string Category { get; set; } = null!;

    /// <summary>
    /// The difficulty level the user selected for this session.
    /// </summary>
    [Required]
    public string Difficulty { get; set; } = null!;

    /// <summary>
    /// Foreign key referencing the chosen question (string ID).
    /// </summary>
    [Required]
    public string QuestionId { get; set; } = null!;

    /// <summary>
    /// Navigation property to the actual Question entity.
    /// </summary>
    [ForeignKey(nameof(QuestionId))]
    public virtual Question? Question { get; set; }

    /// <summary>
    /// When the session was created/started.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// When the session ended (null if still ongoing).
    /// </summary>
    public DateTime? EndedAt { get; set; }

    /// <summary>
    /// How many times the user has attempted to answer.
    /// </summary>
    public int AttemptCount { get; set; } = 0;

    /// <summary>
    /// How many times the user has asked for a hint (optional).
    /// </summary>
    public int HintCount { get; set; } = 0;
}