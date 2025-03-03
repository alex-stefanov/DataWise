using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DataWise.Data.DbContexts.Releational.Models;

public class WiseClient 
    : IdentityUser
{
    [Required]
    [MaxLength(60)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(80)]
    public string LastName { get; set; } = null!;

    public int Points { get; set; }
}
