using Microsoft.AspNetCore.Mvc;
using DTOS = DataWise.Common.DTOs;
using DataWise.Core.Services.Interfaces;
using DataWise.Data.DbContexts.Releational.Models;

namespace DataWise.Api.Controllers;

//TODO: Better Exception Handling

/// <summary>
/// Controller for managing interview sessions.
/// </summary>
/// <param name="interviewService">The interview service instance.</param>
[Route("api/interview")]
[ApiController]
public class InterviewController(
    IInterviewService interviewService)
    : ControllerBase
{
    /// <summary>
    /// Starts a new chat session with a question.
    /// </summary>
    /// <param name="request">The request containing the necessary information to start a chat session.</param>
    /// <returns>Returns the created chat session.</returns>
    /// <response code="200">Chat session successfully started.</response>
    /// <response code="400">Invalid request. UserId, Category, and Difficulty are required.</response>
    /// <response code="500">An unexpected error occurred.</response>
    [HttpPost("start")]
    [ProducesResponseType(typeof(ChatSession), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Start(
        [FromForm]
        DTOS.StartSessionDto request)
    {
        if (request is null
            || string.IsNullOrEmpty(request.UserId)
            || string.IsNullOrEmpty(request.Category)
            || string.IsNullOrEmpty(request.Difficulty))
        {
            return BadRequest("UserId, Category, and Difficulty are required.");
        }

        try
        {
            var session = await interviewService
                .StartChatAsync(
                    userId: request.UserId,
                    category: request.Category,
                    difficulty: request.Difficulty);

            return Ok(session);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Submits an answer to the chat session and evaluates it.
    /// </summary>
    /// <param name="request">The request containing the session ID and the user's answer.</param>
    /// <returns>Returns the feedback after evaluating the answer.</returns>
    /// <response code="200">Answer successfully submitted and evaluated.</response>
    /// <response code="400">Invalid request. SessionId and UserAnswer are required.</response>
    /// <response code="500">An unexpected error occurred.</response>
    [HttpPost("answer")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
    public async Task<IActionResult> Answer(
        [FromBody]
        DTOS.AnswerDto request)
    {
        if (request is null 
            || string.IsNullOrWhiteSpace(request.SessionId) 
            || string.IsNullOrWhiteSpace(request.UserAnswer))
        {
            return BadRequest(new { Message = "SessionId and UserAnswer are required." });
        }

        try
        {
            var feedback = await interviewService.AnswerAsync(request.SessionId, request.UserAnswer);
            return Ok(feedback);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
        }
    }

    /// <summary>
    /// Provides a hint based on the user's last message.
    /// </summary>
    /// <param name="sessionId">The unique identifier of the chat session.</param>
    /// <returns>Returns a hint message to help the user.</returns>
    /// <response code="200">Hint successfully retrieved.</response>
    /// <response code="400">Invalid request. SessionId is required.</response>
    /// <response code="500">An unexpected error occurred.</response>
    [HttpPost("hint")]
    [ProducesResponseType(typeof(ChatMessage), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Hint(
        [FromBody]
        DTOS.HintDto request)
    {
        if (request is null
            || string.IsNullOrWhiteSpace(request.SessionId))
        {
            return BadRequest(new { Message = "SessionId is required." });
        }

        try
        {
            var hint = await interviewService.HintAsync(request.SessionId);
            return Ok(hint);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
        }
    }

    /// <summary>
    /// Retrieves the chat history for a given session.
    /// </summary>
    /// <param name="sessionId">The unique identifier of the chat session.</param>
    /// <returns>Returns the list of messages in the chat session.</returns>
    /// <response code="200">Chat history successfully retrieved.</response>
    /// <response code="400">Invalid request. SessionId is required.</response>
    /// <response code="500">An unexpected error occurred.</response>
    [HttpGet("history/{sessionId}")]
    [ProducesResponseType(typeof(IEnumerable<ChatMessage>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetChatHistory(
        [FromRoute] 
        string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
        {
            return BadRequest(new { Message = "SessionId is required." });
        }

        try
        {
            var history = await interviewService.GetChatHistoryAsync(sessionId);
            return Ok(history);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
        }
    }
}