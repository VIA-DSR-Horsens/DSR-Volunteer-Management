using auth_API.DAO;
using Microsoft.EntityFrameworkCore;

namespace auth_API; 

/// <summary>
/// The class that initializes and acts as the database (sqlite)
/// </summary>
public class AuthenticationContext : DbContext {
    /// <summary>
    /// The users table
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    /// <summary>
    /// To create the class
    /// </summary>
    public AuthenticationContext() { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(
            $"Data Source=authdb.sqlite;"
        );
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var usersE = modelBuilder.Entity<User>();
        usersE.HasKey(e => e.Uuid);
        usersE.HasIndex(e => e.Email).IsUnique();
    }
}