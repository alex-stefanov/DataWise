namespace DataWise.Data.DbContexts.Relational.Enums;

/// <summary>
/// Specifies who sent a particular chat message.
/// </summary>
public enum MessageSender
{
    /// <summary>
    /// Message sent by the user.
    /// </summary>
    User = 0,

    /// <summary>
    /// Message sent by the system (or AI, interviewer, etc.).
    /// </summary>
    System = 1,

    /// <summary>
    /// Message sent specifically by an interviewer persona.
    /// </summary>
    Interviewer = 2,
}

