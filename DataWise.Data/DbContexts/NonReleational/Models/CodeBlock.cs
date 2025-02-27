using System.ComponentModel.DataAnnotations;

namespace DataWise.Data.DbContexts.NonReleational.Models;

public class CodeBlock
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Language { get; set; } = null!;

    [Required]
    public string Code { get; set; } = null!;
}