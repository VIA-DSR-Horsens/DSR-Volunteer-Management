using DatabaseEFC.Exceptions;
using DatabaseEFC.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DatabaseEFC.DAO.Implementations;

public class UserEfcDao : IUserDao
{
    private readonly ManagementContext context;

    public UserEfcDao(ManagementContext context)
    {
        this.context = context;
    }
    public async Task<Volunteer> CreateAsync(DTO.Volunteer volunteer)
    {
        // checking DTO long values
        long volunteerId;
        long rating;
        long shiftsTaken;
        if (!long.TryParse(volunteer.VolunteerId, out volunteerId) ||
            !long.TryParse(volunteer.Rating, out rating) ||
            !long.TryParse(volunteer.ShiftsTaken, out shiftsTaken))
        {
            throw new InvalidDataException("Unparsable volunteerId / rating / shiftsTaken!");
        }

        EntityEntry<Volunteer> newUser = await context.Volunteers.AddAsync(new Volunteer
        {
            VolunteerId = volunteerId,
            Email = volunteer.Email,
            FullName = volunteer.FullName,
            Rating = rating,
            ShiftsTaken = shiftsTaken
        });
        await context.SaveChangesAsync();
        return newUser.Entity;
    }
    
    public async Task<Manager> CreateAsync(DTO.Manager manager)
    {
        // checking DTO long values
        long volunteerId;
        if (!long.TryParse(manager.VolunteerId, out volunteerId))
        {
            throw new InvalidDataException("Unparsable volunteer id!");
        }

        // getting the volunteer
        var volunteer = await context.Volunteers.FindAsync(volunteerId);
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
        // checking DTO long values
        long managerId;
        if (!long.TryParse(administrator.VolunteerId, out managerId))
        {
            throw new InvalidDataException("Unparsable manager id!");
        }
        
        // getting the volunteer
        var volunteer = await context.Volunteers.FindAsync(managerId);
        if (volunteer == null)
            throw new NotFoundException($"Volunteer with id {administrator.VolunteerId} not found!");
        
        EntityEntry<Administrator> newUser = await context.Administrators.AddAsync(new Administrator
        {
            Volunteer = volunteer
        });
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<Volunteer> GetVolunteerAsync(long id)
    {
        // getting the volunteer
        var volunteer = await context.Volunteers.FindAsync(id);
        if (volunteer == null)
            throw new NotFoundException($"Volunteer with id {id} not found!");
        
        return volunteer;
    }
    
    public async Task<Manager> GetManagerAsync(long id)
    {
        // getting the manager
        var manager = await context.Managers.FindAsync(id);
        if (manager == null)
            throw new NotFoundException($"Manager with id {id} not found!");
        
        return manager;
    }
    
    public async Task<Administrator> GetAdministratorAsync(long id)
    {
        // getting the administrator
        var administrator = await context.Administrators.FindAsync(id);
        if (administrator == null)
            throw new NotFoundException($"Administrator with id {id} not found!");
        
        return administrator;
    }
    
    public async Task DeleteManagerByIdAsync(long id)
    {
        var manager = await GetManagerAsync(id);
        // we have to get volunteer id to remove administrator anyway so better to call this method
        await DeleteManagerByVolunteerAsync(manager.Volunteer.VolunteerId);
    }
    
    public async Task DeleteAdministratorByIdAsync(long id)
    {
        var administrator = await GetAdministratorAsync(id);
        context.Administrators.Remove(administrator);
        await context.SaveChangesAsync();
    }
    
    public async Task DeleteManagerByVolunteerAsync(long volunteerId, Manager? theManager = null)
    {
        var volunteer = await GetVolunteerAsync(volunteerId);
        if (volunteer == null)
            throw new NotFoundException($"Volunteer with id {volunteerId} not found!");
        
        // getting the manager
        if (theManager == null)
        {
            IQueryable<Manager> managerQuery = context.Managers.AsQueryable();
            managerQuery = managerQuery.Where(v => v.Volunteer.VolunteerId == volunteerId);
            List<Manager> managerResult = await managerQuery.ToListAsync();
            
            if (managerResult.Count < 1)
            {
                // volunteer isn't a manager
                return;
            }

            theManager = managerResult[0];
        }
        
        
        // getting administrators that match the volunteer id
        IQueryable<Administrator> administratorQuery = context.Administrators.AsQueryable();
        administratorQuery = administratorQuery.Where(v => v.Volunteer.VolunteerId == volunteerId);
        List<Administrator> administratorResult = await administratorQuery.ToListAsync();

        var eventQuery = context.Events.AsQueryable();
        eventQuery = eventQuery.Where(v =>
            v.Managers.Contains(theManager)
        );
        List<Event> eventResult = await eventQuery.ToListAsync();

        // checking do all events have at least 2 managers
        foreach (var e in eventResult)
        {
            if (e.Managers.Count < 2)
                throw new MinimumRequirementsNotMetException($"Event with id {e.EventId} has only 1 manager!");
        }
        
        // removing from administrator role as well
        if (administratorResult.Count > 0)
        {
            context.Administrators.Remove(administratorResult[0]);
        }
        
        // removing the manager
        context.Managers.Remove(theManager);
        await context.SaveChangesAsync();
    }
    
    public async Task DeleteAdministratorByVolunteerAsync(long volunteerId)
    {
        var volunteer = await GetVolunteerAsync(volunteerId);
        if (volunteer == null)
            throw new NotFoundException($"Volunteer with id {volunteerId} not found!");
        
        IQueryable<Administrator> query = context.Administrators.AsQueryable();
        query = query.Where(v => v.Volunteer.VolunteerId == volunteerId);
        List<Administrator> result = await query.ToListAsync();
            
        if (result.Count < 1)
        {
            // volunteer isn't an administrator
            return;
        }
        
        context.Administrators.Remove(result[0]);
        await context.SaveChangesAsync();
    }
}