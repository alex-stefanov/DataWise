using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ENUMS = DataWise.Data.DbContexts.Relational.Enums;

namespace DataWise.Data.DbContexts.Relational.Models;

/// <summary>
/// Represents a single message in the conversation (question, user’s answer, hint, feedback, etc.).
/// </summary>
public class ChatMessage
{
    /// <summary>
    /// Primary key as a string. Defaults to a GUID if not explicitly set.
    /// </summary>
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The ID of the chat session this message belongs to (string).
    /// </summary>
    [Required]
    public string ChatSessionId { get; set; } = null!;

    /// <summary>
    /// Navigation property back to the ChatSession.
    /// </summary>
    [ForeignKey(nameof(ChatSessionId))]
    public virtual ChatSession? ChatSession { get; set; }

    /// <summary>
    /// Who sent the message (User, System, Interviewer, etc.).
    /// </summary>
    [Required]
    public ENUMS.MessageSender Sender { get; set; }

    /// <summary>
    /// The actual text content of the message.
    /// </summary>
    [Required]
    public string Content { get; set; } = null!;

    /// <summary>
    /// Timestamp for when the message was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}