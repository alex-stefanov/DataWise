using DataWise.Core.Services.Interfaces;
using DataWise.Data.DbContexts.Releational.Enums;
using DataWise.Data.DbContexts.Releational.Models;
using DataWise.Data.Repositories.Releational;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenAI_API;

namespace DataWise.Core.Services.Implementations;

/// <summary>
/// Concrete implementation of the IInterviewService for managing chat sessions, answers, and hints.
/// </summary>
public class InterviewService(
    ISQLRepository<ChatSession, string> sessionRepository,
    ISQLRepository<ChatMessage, string> messageRepository,
    ISQLRepository<Question, string> questionRepository,
    UserManager<WiseClient> userManager,
    OpenAIAPI openAIAPI)
    : IInterviewService
{
    /// <inheritdoc />
    public async Task<string> StartChatAsync(
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

        return session.Id;
    }

    /// <inheritdoc />
    public async Task<string> AnswerAsync(
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
            CreatedAt = DateTime.UtcNow,
        };

        await messageRepository.AddAsync(userMsg);

        var question = await questionRepository.GetByIdAsync(session.QuestionId)
            ?? throw new InvalidOperationException("Question not found.");

        var conversation = openAIAPI.Chat.CreateConversation();

        conversation.AppendSystemMessage(
            @"You are an expert interviewer evaluating candidate answers.
            You are a human first of all, so be kind and constructive in your feedback.
            Be generous when giving points for good answers, dont be too harsh and dont look for only one answer of a question.
            Below is a list of 20 possible options. Your response must be in the exact format:
            <number>: <evaluation message>
            Where <number> is an integer from 1 to 16.
            If the candidate's answer is excellent (10 and above), include a congratulatory remark in the message.
            Do not include any additional text.

            The options (for your reference only) are:
            1. '1: Unacceptable – completely incorrect answer.'
            2. '2: Very Poor – lacks understanding.'
            3. '3: Poor – major issues.'
            4. '4: Inadequate – many key points missing.'
            5. '5: Insufficient – only partially correct.'
            6. '6: Incomplete – some important elements missing.'
            7. '7: Fair – meets basic requirements, but improvements needed.'
            8. '8: Good – answer is on the right track; minor adjustments needed.'
            9. '9: Great – strong answer with slight omissions.'
            10. '10: Very Good – almost perfect, minor details missing.'
            11. '11: Excellent – near flawless answer.'
            12. '12: Outstanding – exceeds expectations.'
            13. '13: Exceptional – demonstrates deep understanding.'
            14. '14: Exemplary – impressive and detailed answer.'
            15. '15: Brilliant – superbly crafted answer.'
            16. '16: Perfect – outstanding answer, congratulations!'
            ");

        string prompt = @$"Question: ""{question.QuestionText}""
            Expected Answer: ""{question.AnswerText}""
            Candidate's Answer: ""{userAnswer}""

            Please evaluate the candidate's answer by selecting a rating between 1 and 20 as described above.
            Respond only in the format: <number>: <evaluation message>";
        conversation.AppendUserInput(prompt);

        var response = await conversation.GetResponseFromChatbotAsync();
        string evaluationRaw = response.Trim();

        var (rating, feedback) = ParseEvaluation(evaluationRaw);

        ChatMessage systemMsg;

        session.AttemptCount++;

        if (rating >= 10)
        {
            systemMsg = new ChatMessage
            {
                ChatSessionId = session.Id,
                Sender = MessageSender.System,
                Content = $"Congrats! {feedback}",
                CreatedAt = DateTime.UtcNow
            };

            session.EndedAt = DateTime.UtcNow;

            var user = await userManager
                .FindByIdAsync(session.UserId)
                ?? throw new ArgumentNullException("User not found");

            user.Points += CalculateScore(session, rating);
        }
        else
        {
            systemMsg = new ChatMessage
            {
                ChatSessionId = session.Id,
                Sender = MessageSender.System,
                Content = feedback,
                CreatedAt = DateTime.UtcNow
            };
        }

        await messageRepository.AddAsync(systemMsg);
        sessionRepository.Update(session);

        return session.Id;
    }

    /// <inheritdoc />
    public async Task<string> HintAsync(
        string sessionId)
    {
        var session = await sessionRepository.GetByIdAsync(sessionId)
            ?? throw new InvalidOperationException("Session not found.");

        var question = await questionRepository.GetByIdAsync(session.QuestionId)
            ?? throw new InvalidOperationException("Question not found.");

        var lastUserMessage = await messageRepository.GetAllAttached()
            .Where(m => m.ChatSessionId == sessionId && m.Sender == MessageSender.User)
            .OrderByDescending(m => m.CreatedAt)
            .FirstOrDefaultAsync();

        var userAttempt = lastUserMessage?.Content 
            ?? "No prior user input available.";

        var conversation = openAIAPI.Chat.CreateConversation();

        conversation.AppendSystemMessage(
            @"You are a helpful assistant providing hints for technical interview questions.
            Your goal is to guide the user toward the answer without revealing it completely.
            Make your hints thought-provoking but concise.
            Keep them within 1-2 sentences.
            Use the user's last attempt to provide relevant guidance.");

        string prompt = @$"Question: ""{question.QuestionText}""
            User's last attempt: ""{userAttempt}""
            Provide a hint that helps the user think in the right direction without giving the answer away.";

        conversation.AppendUserInput(prompt);

        var hintResponse = await conversation.GetResponseFromChatbotAsync();

        var hintMsg = new ChatMessage
        {
            ChatSessionId = session.Id,
            Sender = MessageSender.System,
            Content = $"(Hint) {hintResponse}",
            CreatedAt = DateTime.UtcNow,
        };

        session.HintCount++;

        await messageRepository.AddAsync(hintMsg);

        return session.Id;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ChatMessage>> GetChatHistoryAsync(
        string sessionId)
    {
        var all = await messageRepository.GetAllAttached()
            .Where(m => m.ChatSessionId == sessionId)
            .OrderBy(m => m.CreatedAt)
            .ToListAsync();

        return all;
    }

    /// <summary>
    /// Parses the evaluation response from OpenAI.
    /// </summary>
    /// <param name="evaluationRaw">The raw evaluation response (expected in format: "number: message").</param>
    /// <returns>A tuple containing the numeric rating and the feedback message.</returns>
    private static (int rating, string feedback) ParseEvaluation(
        string evaluationRaw)
    {
        var parts = evaluationRaw.Split(':', 2);
        if (parts.Length != 2)
        {
            return (0, "Unable to parse evaluation response.");
        }

        if (int.TryParse(parts[0].Trim(), out int rating))
        {
            string feedback = parts[1].Trim();
            return (rating, feedback);
        }

        return (0, "Unable to parse evaluation rating.");
    }

    private static int CalculateScore(
        ChatSession session,
        int rating)
    {
        int difficultyValue = session.Difficulty switch
        {
            "Easy" => 1,
            "Medium" => 2,
            "Hard" => 4,
            "Extremely Hard" => 8,
            _ => 1
        };

        int baseScore = (int)Math.Floor(10 * Math.Log2(difficultyValue + 1));

        double categoryMultiplier = session.Category switch
        {
            "DevOps" => 1 + Math.Log2(6) / 5,
            "Containers and Cloud" => 1 + Math.Log2(5) / 5,
            "SQL" => 1 + Math.Log2(4) / 5,
            "AI" => 1 + Math.Log2(3) / 5,
            "General Software Engineering" => 1 + Math.Log2(2) / 5,
            _ => 1.0
        };

        double attemptsMultiplier = session.AttemptCount switch
        {
            1 => 1.2,
            2 => 1.0,
            3 => 0.9,
            _ => Math.Max(0.5, 0.8 - 0.1 * Math.Log2(session.AttemptCount))
        };

        double correctnessMultiplier = Math.Exp((rating - 20) / 50.0);

        double hintPenalty = session.HintCount switch
        {
            0 => 1.0,
            1 => 0.8,
            _ => Math.Max(0.2, 0.5 / session.HintCount)
        };

        int finalScore = (int)Math.Max(1, Math.Floor(baseScore * categoryMultiplier * attemptsMultiplier * correctnessMultiplier * hintPenalty));

        return finalScore;
    }
}