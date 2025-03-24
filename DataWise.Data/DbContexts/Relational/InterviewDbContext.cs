using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MODELS = DataWise.Data.DbContexts.Relational.Models;

namespace DataWise.Data.DbContexts.Relational;

/// <summary>
/// Represents the application's database context for both user management (Identity)
/// and the interview Q&A system (Questions, ChatSessions, ChatMessages).
/// </summary>
public class InterviewDbContext
    : IdentityDbContext<MODELS.WiseClient, IdentityRole<string>, string>
{
    /// <summary>
    /// Gets or sets the DbSet of <see cref="MODELS.WiseClient"/> entities (Identity users).
    /// </summary>
    public override DbSet<MODELS.WiseClient> Users { get; set; } = null!;

    /// <summary>
    /// The table of questions (seeded from the CSV).
    /// </summary>
    public DbSet<MODELS.Question> Questions { get; set; } = null!;

    /// <summary>
    /// The table of chat sessions.
    /// </summary>
    public DbSet<MODELS.ChatSession> ChatSessions { get; set; } = null!;

    /// <summary>
    /// The table of chat messages.
    /// </summary>
    public DbSet<MODELS.ChatMessage> ChatMessages { get; set; } = null!;

    public InterviewDbContext() { }

    public InterviewDbContext(
        DbContextOptions<InterviewDbContext> options)
        : base(options) { }

    /// <summary>
    /// Configures the model and the relationships between entities.
    /// </summary>
    /// <param name="modelBuilder">The <see cref="ModelBuilder"/> to configure the model with.</param>
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}