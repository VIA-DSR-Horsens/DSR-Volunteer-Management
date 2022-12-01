using DatabaseEFC.Exceptions;
using DatabaseEFC.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DatabaseEFC.DAO;

public class EventEfcDao : IEventDao
{
    private readonly ManagementContext context;

    public EventEfcDao(ManagementContext context)
    {
        this.context = context;
    }
    
    public async Task<Event> GetByIdAsync(long id)
    {
        IQueryable<Event> query = context.Events.AsQueryable();
        query = query.Where(v => v.EventId == id);
        List<Event> result = await query.ToListAsync();
        
        if (result.Count < 1)
        {
            throw new NotFoundException($"Event with id {id} not found!");
        }
        return result[0];
    }

    public async Task<Event> CreateAsync(DTO.Event eventDTO)
    {
        // converting to DAO event
        Event ev = new Event
        {
            Date = eventDTO.Date,
            EventName = eventDTO.EventName
        };
        if (eventDTO.Location != null)
            ev.Location = eventDTO.Location;
        if (eventDTO.StartTime != null)
            ev.StartTime = eventDTO.StartTime;
        if (eventDTO.EndTime != null)
            ev.EndTime = eventDTO.EndTime;
        
        // getting managers
        var managers = new List<Manager>();
        // TODO: Convert to a single query
        foreach (var mId in eventDTO.Managers)
        {
            IQueryable<Manager> query = context.Managers.AsQueryable();
            query = query.Where(v => v.ManagerId == mId);
            List<Manager> result = await query.ToListAsync();
            
            if (result.Count < 1)
            {
                throw new NotFoundException($"Manager with id {mId} not found!");
            }
            managers.Add(result[0]);
        }
        // adding managers to event
        ev.Managers = managers;
        
        // getting shifts
        if (eventDTO.Shifts != null)
        {
            var shifts = new List<Shift>();
            // TODO: Convert to a single query
            foreach (var sId in eventDTO.Shifts)
            {
                IQueryable<Shift> query = context.Shifts.AsQueryable();
                query = query.Where(v => v.ShiftId == sId);
                List<Shift> result = await query.ToListAsync();
            
                if (result.Count < 1)
                {
                    throw new NotFoundException($"Shift with id {sId} not found!");
                }
                shifts.Add(result[0]);
            }
            // adding shifts to event
            ev.Shifts = shifts;
        }

        // attempting to create the new event in database
        EntityEntry<Event> newUser = await context.Events.AddAsync(ev);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }
}