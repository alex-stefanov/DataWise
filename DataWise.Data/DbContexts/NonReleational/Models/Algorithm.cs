using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using DataWise.Data.DbContexts.NonReleational.Enums;

namespace DataWise.Data.DbContexts.NonReleational.Models;

public class Algorithm
{
    [BsonId]
    [BsonRequired]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonRequired]
    [BsonElement("name")]
    public string Name { get; set; } = null!;

    [BsonRequired]
    [BsonElement("description")]
    public string Description { get; set; } = null!;

    [BsonRequired]
    [BsonElement("complexity")]
    public Complexity Complexity { get; set; }

    [BsonElement("useCases")]
    public List<string> UseCases { get; set; } = [];

    [BsonElement("codeBlock")]
    public List<CodeBlock> CodeBlocks { get; set; } = [];

    [BsonElement("category")]
    public AlgorithmCategory Category { get; set; } = null!;
}