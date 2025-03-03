using DataWise.Data.DbContexts.Releational.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataWise.Data.DbContexts.Releational;

public class UserDbContext
    : IdentityDbContext<WiseClient, IdentityRole<string>, string>
{
    public override DbSet<WiseClient> Users { get; set; }

    public UserDbContext() { }

    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}