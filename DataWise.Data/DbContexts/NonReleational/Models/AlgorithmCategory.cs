using MongoDB.Bson.Serialization.Attributes;

namespace DataWise.Data.DbContexts.NonReleational.Models;

public class AlgorithmCategory
{
    [BsonRequired]
    [BsonElement("name")]
    public string Name { get; set; } = null!;

    [BsonRequired]
    [BsonElement("description")]
    public string Description { get; set; } = null!;
}