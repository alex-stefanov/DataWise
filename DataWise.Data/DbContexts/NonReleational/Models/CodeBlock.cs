using System.ComponentModel.DataAnnotations;

namespace DataWise.Data.DbContexts.NonReleational.Models;

/// <summary>
/// Represents a code block for an algorithm in a specific programming language.
/// </summary>
public class CodeBlock
{
    /// <summary>
    /// Gets or sets the programming language of the code block.
    /// The language should be specified as a string (e.g., "C#", "Python", etc.).
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Language { get; set; } = null!;

    /// <summary>
    /// Gets or sets the actual code written in the specified language.
    /// The code block represents the algorithm implementation in the given programming language.
    /// </summary>
    [Required]
    public string Code { get; set; } = null!;
}