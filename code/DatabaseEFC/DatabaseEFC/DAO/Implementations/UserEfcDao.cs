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
        if (!long.TryParse(volunteer.VolunteerId, out volunteerId))
        {
            throw new InvalidDataException("Unparsable volunteerId!");
        }

        EntityEntry<Volunteer> newUser = await context.Volunteers.AddAsync(new Volunteer
        {
            VolunteerId = volunteerId,
            Email = volunteer.Email,
            FullName = volunteer.FullName
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
        var volunteer = await GetVolunteerAsync(volunteerId);
        
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
        long volunteerId;
        if (!long.TryParse(administrator.VolunteerId, out volunteerId))
        {
            throw new InvalidDataException("Unparsable volunteer id!");
        }
        
        // getting the volunteer
        var volunteer = await GetVolunteerAsync(volunteerId);
        Manager manager;
        try
        {
            manager = await GetManagerByVolunteerAsync(volunteerId);
            // volunteer is already a manager, proceed with creating administrator
        }
        catch (NotFoundException e)
        {
            // volunteer isnt a manager, adding as manager first
            manager = await CreateAsync(new DTO.Manager {VolunteerId = volunteerId+""});
        }
        
        // creating the manager
        EntityEntry<Administrator> newUser = await context.Administrators.AddAsync(new Administrator
        {
            Volunteer = volunteer,
            Manager = manager
        });
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<Volunteer> GetVolunteerAsync(long id)
    {
        // getting the volunteer
        var query = context.Volunteers.Include(v => v.Shifts)
            .AsQueryable();
        var volunteers = await query.Where(v => v.VolunteerId == id).ToListAsync();
        if (volunteers.Count < 1)
            throw new NotFoundException($"Volunteer with id {id} not found!");
        
        return volunteers[0];
    }
    
    public async Task<Manager> GetManagerAsync(long id)
    {
        // getting the manager
        var query = context.Managers.Include(v => v.Volunteer)
            .Include(v => v.EventsManaged)
            .AsQueryable();
        var managers = await query.Where(v => v.ManagerId == id).ToListAsync();

        if (managers.Count < 1)
            throw new NotFoundException($"Manager with id {id} not found!");
        
        return managers[0];
    }

    public async Task<Manager> GetManagerByVolunteerAsync(long id)
    {
        // checking does the volunteer actually exist
        await GetVolunteerAsync(id);
        
        // getting the manager
        var query = context.Managers.Include(v => v.Volunteer)
            .Include(v => v.EventsManaged)
            .AsQueryable();
        var managers = await query.Where(v => v.Volunteer.VolunteerId == id).ToListAsync();

        if (managers.Count < 1)
            throw new NotFoundException($"Manager with volunteer id {id} not found!");

        return managers[0];
    }
    
    public async Task<Administrator> GetAdministratorAsync(long id)
    {
        // getting the administrator
        var query = context.Administrators.Include(v => v.Volunteer)
            .Include(v => v.Manager)
            .AsQueryable();
        var administrators = await query.Where(v => v.AdministratorId == id).ToListAsync();

        if (administrators.Count < 1)
            throw new NotFoundException($"Administrator with id {id} not found!");
        
        return administrators[0];
    }
    
    public async Task<Administrator> GetAdministratorByVolunteerAsync(long id)
    {
        // checking does the volunteer actually exist
        await GetVolunteerAsync(id);
        
        // getting the administrator
        var query = context.Administrators.Include(v => v.Volunteer)
            .Include(v => v.Manager)
            .AsQueryable();
        var administrators = await query.Where(v => v.Volunteer.VolunteerId == id).ToListAsync();

        if (administrators.Count < 1)
            throw new NotFoundException($"Administrator with volunteer id {id} not found!");
        
        return administrators[0];
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
        // just to verify that the volunteer exists
        await GetVolunteerAsync(volunteerId);

        // getting the manager
        if (theManager == null)
        {
            IQueryable<Manager> managerQuery = context.Managers.Include(v => v.Volunteer)
                .Include(v => v.EventsManaged)
                .AsQueryable();
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
        IQueryable<Administrator> administratorQuery = context.Administrators.Include(v => v.Volunteer)
            .AsQueryable();
        administratorQuery = administratorQuery.Where(v => v.Volunteer.VolunteerId == volunteerId);
        List<Administrator> administratorResult = await administratorQuery.ToListAsync();

        var eventQuery = context.Events.Include(v => v.Managers)
            .AsQueryable();
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
        // just checking does the volunteer actually exist
        await GetVolunteerAsync(volunteerId);

        IQueryable<Administrator> query = context.Administrators.Include(v => v.Volunteer)
            .AsQueryable();
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