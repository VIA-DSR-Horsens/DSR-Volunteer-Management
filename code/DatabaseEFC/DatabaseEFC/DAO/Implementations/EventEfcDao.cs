using DatabaseEFC.Exceptions;
using DatabaseEFC.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DatabaseEFC.DAO.Implementations;

public class EventEfcDao : IEventDao
{
    private readonly ManagementContext context;

    public EventEfcDao(ManagementContext context)
    {
        this.context = context;
    }
    
    public async Task<Event> GetByIdAsync(long id)
    {
        var query = context.Events.Include(v => v.Managers)
            .Include(v => v.Shifts)
            .AsQueryable();
        var events = await query.Where(v => v.EventId == id).ToListAsync();
        if (events.Count < 1)
            throw new NotFoundException($"Event with id {id} not found!");
        
        return events[0];
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
        foreach (var mId in eventDTO.Managers)
        {
            long managerId;
            if (!long.TryParse(mId, out managerId))
            {
                throw new InvalidDataException($"Manager with id {mId} couldn't be parsed!");
            }
                
            var manager = await context.Managers.FindAsync(managerId);
            if (manager == null)
            {
                throw new NotFoundException($"Manager with id {mId} not found!");
            }
            managers.Add(manager);
        }
        // adding managers to event
        ev.Managers = managers;
        
        // getting shifts
        if (eventDTO.Shifts != null)
        {
            var shifts = new List<Shift>();
            foreach (var sId in eventDTO.Shifts)
            {
                long shiftId;
                if (!long.TryParse(sId, out shiftId))
                {
                    throw new InvalidDataException($"Shift with id {sId} couldn't be parsed!");
                }
                
                var shift = await context.Shifts.FindAsync(shiftId);
                if (shift == null)
                {
                    throw new NotFoundException($"Shift with id {sId} not found!");
                }
                shifts.Add(shift);
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