using MongoDB.Bson.Serialization.Attributes;

namespace DataWise.Data.DbContexts.NonReleational.Models;

/// <summary>
/// Represents the category of an algorithm in the Knowledge Nexus database.
/// </summary>
public class AlgorithmCategory
{
    /// <summary>
    /// Gets or sets the name of the algorithm category.
    /// </summary>
    [BsonRequired]
    [BsonElement("name")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the algorithm category.
    /// </summary>
    [BsonRequired]
    [BsonElement("description")]
    public string Description { get; set; } = null!;
}