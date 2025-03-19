using System.ComponentModel.DataAnnotations;

namespace DataWise.Data.DbContexts.Releational.Models;

/// <summary>
/// Represents a question imported from the CSV (category, difficulty, etc.).
/// </summary>
public class Question
{
    /// <summary>
    /// Primary key as a string. Defaults to a GUID if not explicitly set.
    /// </summary>
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The category of the question (e.g., AI, SQL, DevOps, etc.).
    /// </summary>
    [Required]
    public string Category { get; set; } = null!;

    /// <summary>
    /// The difficulty level (e.g., Easy, Medium, Hard, etc.).
    /// </summary>
    [Required]
    public string Difficulty { get; set; } = null!;

    /// <summary>
    /// The text of the question itself.
    /// </summary>
    [Required]
    public string QuestionText { get; set; } = null!;

    /// <summary>
    /// The correct answer (for AI validation and reference).
    /// </summary>
    [Required]
    public string AnswerText { get; set; } = null!;
}