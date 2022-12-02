using DatabaseEFC.Utils;
using Microsoft.EntityFrameworkCore;

namespace DatabaseEFC;

public class ManagementContext : DbContext
{
    private DbCredentials dbCreds;
    public DbSet<Volunteer> Volunteers { get; set; } = null!;
    public DbSet<Manager> Managers { get; set; } = null!;
    public DbSet<Administrator> Administrators { get; set; } = null!;
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<Shift> Shifts { get; set; } = null!;

    public ManagementContext()
    {
        dbCreds = Program.GetDbCredentials("dbconnection.json");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            $"Host={dbCreds.Host};Database={dbCreds.Database};Username={dbCreds.Username};Password={dbCreds.Password}"
            );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(dbCreds.Schema); // setting the default schema
        var eventE = modelBuilder.Entity<Event>();
        eventE.HasKey(e => e.EventId);
        
        var volunteerE = modelBuilder.Entity<Volunteer>();
        volunteerE.HasKey(e => e.VolunteerId);
        
        var managerE = modelBuilder.Entity<Manager>();
        managerE.HasKey(e => e.ManagerId);
        
        var administratorE = modelBuilder.Entity<Administrator>();
        administratorE.HasKey(e => e.AdministratorId);
        
        var shiftE = modelBuilder.Entity<Shift>();
        shiftE.HasKey(e => e.ShiftId);
    }
}