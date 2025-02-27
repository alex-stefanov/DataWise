using System.ComponentModel.DataAnnotations;
using DataWise.Data.DbContexts.NonReleational.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataWise.Data.DbContexts.NonReleational.Models;

public class DataStructure
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(600)]
    public string Description { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;

    [Required]
    public string Example { get; set; } = null!;

    [Required]
    public Complexity AccessTimeComplexity { get; set; }

    [Required]
    public Complexity InsertionTimeComplexity { get; set; }

    [Required]
    public Complexity DeletionTimeComplexity { get; set; }

    public List<CodeBlock> CodeBlocks { get; set; } = [];

    public List<DataStructureSubType> SubTypes { get; set; } = [];
}