using System.ComponentModel.DataAnnotations;
using NR_ENUMS = DataWise.Data.DbContexts.NonReleational.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataWise.Data.DbContexts.NonReleational.Models;

/// <summary>
/// Represents a data structure used in algorithms.
/// Contains details such as name, description, complexities, and related code blocks.
/// </summary>
public class DataStructure
{
    /// <summary>
    /// Gets or sets the unique identifier for the data structure.
    /// This ID is automatically generated as an ObjectId by MongoDB.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the data structure.
    /// This is a required property with a maximum length of 100 characters.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the data structure.
    /// This is a required property with a maximum length of 600 characters.
    /// </summary>
    [Required]
    [MaxLength(600)]
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or sets the URL of the image representing the data structure.
    /// This is a required property.
    /// </summary>
    [Required]
    public string ImageUrl { get; set; } = null!;

    /// <summary>
    /// Gets or sets an example of the data structure in use.
    /// This is a required property.
    /// </summary>
    [Required]
    public string Example { get; set; } = null!;

    /// <summary>
    /// Gets or sets the access time complexity of the data structure.
    /// This is a required property, represented by a value from the <see cref="NR_ENUMS.Complexity"/> enum.
    /// </summary>
    [Required]
    public NR_ENUMS.Complexity AccessTimeComplexity { get; set; }

    /// <summary>
    /// Gets or sets the insertion time complexity of the data structure.
    /// This is a required property, represented by a value from the <see cref="NR_ENUMS.Complexity"/> enum.
    /// </summary>
    [Required]
    public NR_ENUMS.Complexity InsertionTimeComplexity { get; set; }

    /// <summary>
    /// Gets or sets the deletion time complexity of the data structure.
    /// This is a required property, represented by a value from the <see cref="NR_ENUMS.Complexity"/> enum.
    /// </summary>
    [Required]
    public NR_ENUMS.Complexity DeletionTimeComplexity { get; set; }

    /// <summary>
    /// Gets or sets a list of code blocks demonstrating the data structure's implementation.
    /// </summary>
    public List<CodeBlock> CodeBlocks { get; set; } = [];

    /// <summary>
    /// Gets or sets a list of subtypes for the data structure.
    /// </summary>
    public List<DataStructureSubType> SubTypes { get; set; } = [];
}