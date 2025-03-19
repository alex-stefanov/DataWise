using DataWise.Core.Services.Interfaces;
using DataWise.Data.DbContexts.Releational.Enums;
using DataWise.Data.DbContexts.Releational.Models;
using DataWise.Data.Repositories.Releational;

namespace DataWise.Core.Services.Implementations;

/// <summary>
/// Concrete implementation of the IInterviewService for managing chat sessions, answers, and hints.
/// </summary>
public class InterviewService(
    ISQLRepository<ChatSession, string> sessionRepository,
    ISQLRepository<ChatMessage, string> messageRepository,
    ISQLRepository<Question, string> questionRepository)
    : IInterviewService
{
    /// <inheritdoc />
    public async Task<ChatSession> StartChatAsync(
        string userId,
        string category,
        string difficulty)
    {
        var allMatching = await questionRepository.GetAllAsync();
        var filtered = allMatching
            .Where(q => q.Category.Equals(category, StringComparison.OrdinalIgnoreCase)
                     && q.Difficulty.Equals(difficulty, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (filtered.Count == 0)
            throw new InvalidOperationException("No questions available for the given category/difficulty.");

        var random = new Random();
        var index = random.Next(filtered.Count);
        var chosenQuestion = filtered[index];

        var session = new ChatSession
        {
            UserId = userId,
            Category = category,
            Difficulty = difficulty,
            QuestionId = chosenQuestion.Id,
            CreatedAt = DateTime.UtcNow
        };

        await sessionRepository
            .AddAsync(session);

        var message = new ChatMessage
        {
            ChatSessionId = session.Id,
            Sender = MessageSender.System,
            Content = chosenQuestion.QuestionText,
            CreatedAt = DateTime.UtcNow
        };

        await messageRepository
            .AddAsync(message);

        return session;
    }

    /// <inheritdoc />
    public async Task<ChatMessage> AnswerAsync(
        string sessionId,
        string userAnswer)
    {
        var session = await sessionRepository.GetByIdAsync(sessionId) 
            ?? throw new InvalidOperationException("Session not found.");

        var userMsg = new ChatMessage
        {
            ChatSessionId = session.Id,
            Sender = MessageSender.User,
            Content = userAnswer,
            CreatedAt = DateTime.UtcNow
        };

        await messageRepository.AddAsync(userMsg);

        var question = await questionRepository.GetByIdAsync(session.QuestionId) 
            ?? throw new InvalidOperationException("Question not found.");

        bool isCorrect = userAnswer
            .Contains(question.AnswerText, StringComparison.OrdinalIgnoreCase);

        var feedback = isCorrect
            ? "Correct! (Placeholder logic)"
            : "Incorrect, try again or ask for a hint.";

        var systemMsg = new ChatMessage
        {
            ChatSessionId = session.Id,
            Sender = MessageSender.System,
            Content = feedback,
            CreatedAt = DateTime.UtcNow
        };

        await messageRepository.AddAsync(systemMsg);

        session.AttemptCount++;
        sessionRepository.Update(session);

        return systemMsg;
    }

    /// <inheritdoc />
    public async Task<ChatMessage> HintAsync(string sessionId)
    {
        var session = await sessionRepository.GetByIdAsync(sessionId) 
            ?? throw new InvalidOperationException("Session not found.");

        var question = await questionRepository.GetByIdAsync(session.QuestionId) 
            ?? throw new InvalidOperationException("Question not found.");

        var hintText = $"(Hint) Think about keywords in: {question.QuestionText}";

        var hintMsg = new ChatMessage
        {
            ChatSessionId = session.Id,
            Sender = MessageSender.System,
            Content = hintText,
            CreatedAt = DateTime.UtcNow
        };

        await messageRepository
            .AddAsync(hintMsg);

        return hintMsg;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ChatMessage>> GetChatHistoryAsync(
        string sessionId)
    {
        var all = messageRepository.GetAllAttached()
            .Where(m => m.ChatSessionId == sessionId)
            .OrderBy(m => m.CreatedAt);

        return await Task.FromResult(all.ToList());
    }
}