using DataWise.Data.DbContexts.Releational.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataWise.Data.DbContexts.Releational;

/// <summary>
/// Represents the application's database context for user management, extending the IdentityDbContext
/// to include custom configurations for the <see cref="WiseClient"/> and other identity-related entities.
/// </summary>
public class UserDbContext 
    : IdentityDbContext<WiseClient, IdentityRole<string>, string>
{
    /// <summary>
    /// Gets or sets the DbSet of <see cref="WiseClient"/> entities.
    /// This DbSet represents the collection of user records in the database.
    /// </summary>
    public override DbSet<WiseClient> Users { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserDbContext"/> class.
    /// </summary>
    public UserDbContext() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserDbContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to configure the database context.</param>
    public UserDbContext(
        DbContextOptions<UserDbContext> options)
        : base(options) { }

    /// <summary>
    /// Configures the model and the relationships between entities.
    /// This method is called during model creation to configure entity relationships, 
    /// constraints, and other settings for the <see cref="WiseClient"/> and related entities.
    /// </summary>
    /// <param name="modelBuilder">The <see cref="ModelBuilder"/> to configure the model with.</param>
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}