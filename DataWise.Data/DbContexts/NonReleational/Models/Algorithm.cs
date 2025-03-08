using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NR_ENUMS = DataWise.Data.DbContexts.NonReleational.Enums;

namespace DataWise.Data.DbContexts.NonReleational.Models;

/// <summary>
/// Represents an algorithm in the Knowledge Nexus database.
/// </summary>
public class Algorithm
{
    /// <summary>
    /// Gets or sets the unique identifier for the algorithm.
    /// </summary>
    [BsonId]
    [BsonRequired]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the algorithm.
    /// </summary>
    [BsonRequired]
    [BsonElement("name")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the algorithm.
    /// </summary>
    [BsonRequired]
    [BsonElement("description")]
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or sets the complexity level of the algorithm.
    /// </summary>
    [BsonRequired]
    [BsonElement("complexity")]
    public NR_ENUMS.Complexity Complexity { get; set; }

    /// <summary>
    /// Gets or sets the use cases associated with the algorithm.
    /// </summary>
    [BsonElement("useCases")]
    public List<string> UseCases { get; set; } = new();

    /// <summary>
    /// Gets or sets the code blocks that demonstrate the algorithm.
    /// </summary>
    [BsonElement("codeBlock")]
    public List<CodeBlock> CodeBlocks { get; set; } = new();

    /// <summary>
    /// Gets or sets the category of the algorithm.
    /// </summary>
    [BsonElement("category")]
    public AlgorithmCategory Category { get; set; } = null!;
}