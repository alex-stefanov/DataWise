using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DataWise.Data.DbContexts.Relational.Models;

/// <summary>
/// Represents a user in the application, extending the <see cref="IdentityUser"/> class
/// to include additional properties for the user's first name, last name, and points.
/// </summary>
public class WiseClient 
    : IdentityUser
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// This property is required and has a maximum length of 60 characters.
    /// </summary>
    [Required]
    [MaxLength(60)]
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the last name of the user.
    /// This property is required and has a maximum length of 80 characters.
    /// </summary>
    [Required]
    [MaxLength(80)]
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the points associated with the user.
    /// This property tracks the user's points and is stored as an integer.
    /// </summary>
    public int Points { get; set; }
}