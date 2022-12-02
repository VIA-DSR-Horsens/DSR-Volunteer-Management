using DatabaseEFC.Exceptions;
using DatabaseEFC.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DatabaseEFC.DAO;

public class UserEfcDao : IUserDao
{
    private readonly ManagementContext context;

    public UserEfcDao(ManagementContext context)
    {
        this.context = context;
    }
    
    public async Task<Volunteer> CreateAsync(DTO.Volunteer volunteer)
    {
        EntityEntry<Volunteer> newUser = await context.Volunteers.AddAsync(new Volunteer
        {
           Email = volunteer.Email,
           FullName = volunteer.FullName,
           Rating = volunteer.Rating,
           ShiftsTaken = volunteer.ShiftsTaken
        });
        await context.SaveChangesAsync();
        return newUser.Entity;
    }
    
    public async Task<Manager> CreateAsync(DTO.Manager manager)
    {
        // getting the volunteer
        var volunteer = context.Volunteers.Find(manager.VolunteerId);
        if (volunteer == null)
            throw new NotFoundException($"Volunteer with id {manager.VolunteerId} not found!");
        
        EntityEntry<Manager> newUser = await context.Managers.AddAsync(new Manager
        {
            Volunteer = volunteer
        });
        await context.SaveChangesAsync();
        return newUser.Entity;
    }
    
    public async Task<Administrator> CreateAsync(DTO.Administrator administrator)
    {
        // getting the administrator
        var manager = context.Managers.Find(administrator.ManagerId);
        if (manager == null)
            throw new NotFoundException($"Manager with id {administrator.ManagerId} not found!");
        
        EntityEntry<Administrator> newUser = await context.Administrators.AddAsync(new Administrator
        {
            Manager = manager
        });
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<Volunteer> UpdateAsync(Volunteer volunteer)
    {
        EntityEntry<Volunteer> updatedUser = context.Volunteers.Update(volunteer);
        await context.SaveChangesAsync();
        return updatedUser.Entity;
    }

    public async Task<Volunteer> GetVolunteerAsync(long id)
    {
        IQueryable<Volunteer> query = context.Volunteers.AsQueryable();
        query = query.Where(v => v.VolunteerId == id);
        List<Volunteer> result = await query.ToListAsync();
        
        if (result.Count < 1)
        {
            throw new NotFoundException($"Volunteer with id {id} not found!");
        }
        return result[0];
    }
    
    public async Task<Manager> GetManagerAsync(long id)
    {
        IQueryable<Manager> query = context.Managers.AsQueryable();
        query = query.Where(v => v.ManagerId == id);
        List<Manager> result = await query.ToListAsync();
        
        if (result.Count < 1)
        {
            throw new NotFoundException($"Manager with id {id} not found!");
        }
        return result[0];
    }
    
    public async Task<Administrator> GetAdministratorAsync(long id)
    {
        IQueryable<Administrator> query = context.Administrators.AsQueryable();
        query = query.Where(v => v.AdministratorId == id);
        List<Administrator> result = await query.ToListAsync();
        
        if (result.Count < 1)
        {
            throw new NotFoundException($"Administrator with id {id} not found!");
        }
        return result[0];
    }
}