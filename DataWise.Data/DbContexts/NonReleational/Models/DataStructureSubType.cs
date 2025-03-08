using System.ComponentModel.DataAnnotations;

namespace DataWise.Data.DbContexts.NonReleational.Models;

/// <summary>
/// Represents a subtype of a data structure.
/// Contains details such as title, description, differences, and related code blocks.
/// </summary>
public class DataStructureSubType
{
    /// <summary>
    /// Gets or sets the title of the data structure subtype.
    /// This is a required property with a maximum length of 100 characters.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the data structure subtype.
    /// This is a required property with a maximum length of 600 characters.
    /// </summary>
    [Required]
    [MaxLength(600)]
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or sets the differences between this subtype and the main data structure.
    /// This is a required property with a maximum length of 400 characters.
    /// </summary>
    [Required]
    [MaxLength(400)]
    public string Differences { get; set; } = null!;

    /// <summary>
    /// Gets or sets a list of code blocks demonstrating the implementation of this data structure subtype.
    /// </summary>
    public List<CodeBlock> CodeBlocks { get; set; } = [];
}