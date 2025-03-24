using MODELS = DataWise.Data.DbContexts.Relational.Models;

namespace DataWise.Core.Services.Interfaces;

/// <summary>
/// Defines the contract for managing interview chats, questions, answers, and hints.
/// </summary>
public interface IInterviewService
{
    /// <summary>
    /// Starts a new chat session by picking a random question and storing it in the DB.
    /// </summary>
    /// <param name="userId">The user initiating the chat.</param>
    /// <param name="category">The selected category.</param>
    /// <param name="difficulty">The selected difficulty.</param>
    /// <returns>The newly created sessionId.</returns>
    Task<string> StartChatAsync(
        string userId,
        string category,
        string difficulty);

    /// <summary>
    /// Submits an answer for a specific chat session.
    /// </summary>
    /// <param name="sessionId">The ID of the chat session.</param>
    /// <param name="userAnswer">The user's answer.</param>
    /// <returns>The sessionId.</returns>
    Task<string> AnswerAsync(
        string sessionId,
        string userAnswer);

    /// <summary>
    /// Requests a hint for the question in the specified chat session.
    /// </summary>
    /// <param name="sessionId">The ID of the chat session.</param>
    /// <returns>The sessionId.</returns>
    Task<string> HintAsync(
        string sessionId);

    /// <summary>
    /// Retrieves the entire chat history (messages) for a given session.
    /// </summary>
    /// <param name="sessionId">The ID of the chat session.</param>
    /// <returns>An enumerable collection of <see cref="MODELS.ChatMessage"/>.</returns>
    Task<IEnumerable<MODELS.ChatMessage>> GetChatHistoryAsync(
        string sessionId);
}