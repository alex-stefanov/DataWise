using System.ComponentModel.DataAnnotations;

namespace DataWise.Data.DbContexts.NonReleational.Models;

public class DataStructureSubType
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(600)]
    public string Description { get; set; } = null!;

    [Required]
    [MaxLength(400)]
    public string Differences { get; set; } = null!;

    public List<CodeBlock> CodeBlocks { get; set; } = [];
}