using DatabaseEFC.Exceptions;
using DatabaseEFC.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DatabaseEFC.DAO.Implementations;

public class ShiftEfcDao : IShiftDao
{
    private readonly ManagementContext context;

    public ShiftEfcDao(ManagementContext context)
    {
        this.context = context;
    }
    
    public async Task<Shift> GetAsync(long id)
    {
        IQueryable<Shift> query = context.Shifts.AsQueryable();
        query = query.Where(v => v.ShiftId == id);
        List<Shift> result = await query.ToListAsync();
        
        if (result.Count < 1)
        {
            throw new NotFoundException($"Shift with id {id} not found!");
        }
        return result[0];
    }

    public async Task<Shift> CreateAsync(DTO.Shift shiftDTO)
    {
        // converting to DAO shift
        Shift sh = new Shift
        {
            Accepted = shiftDTO.Accepted,
            EndTime = shiftDTO.EndTime,
            StartTime = shiftDTO.StartTime
        };
        
        
        // getting event
        IQueryable<Event> eventQuery = context.Events.AsQueryable();
        eventQuery = eventQuery.Where(v => v.EventId+"" == shiftDTO.EventId);
        List<Event> eventResult = await eventQuery.ToListAsync();

        if (eventResult.Count < 1)
        {
            throw new NotFoundException($"Event with id {shiftDTO.EventId} not found!");
        }

        sh.Event = eventResult[0];
        
        // getting volunteer
        IQueryable<Volunteer> volunteerQuery = context.Volunteers.AsQueryable();
        volunteerQuery = volunteerQuery.Where(v => v.VolunteerId+"" == shiftDTO.VolunteerId);
        List<Volunteer> volunteerResult = await volunteerQuery.ToListAsync();
        
        if (volunteerResult.Count < 1)
        {
            throw new NotFoundException($"Volunteer with id {shiftDTO.VolunteerId} not found!");
        }

        sh.Volunteer = volunteerResult[0];

        // attempting to create the new shift in database
        EntityEntry<Shift> newShift = await context.Shifts.AddAsync(sh);
        await context.SaveChangesAsync();
        return newShift.Entity;
    }
    
    public async Task<Shift> UpdateAsync(DTO.Shift shiftDTO)
    {
        // checking for shift id
        if (shiftDTO.ShiftId == null)
        {
            throw new NotFoundException("Existing shift not found! No id provided.");
        }
        
        // getting existing shift info
        IQueryable<Shift> shiftQuery = context.Shifts.AsQueryable();
        shiftQuery = shiftQuery.Where(v => v.ShiftId+"" == shiftDTO.ShiftId);
        List<Shift> shiftResult = await shiftQuery.ToListAsync();
        
        if (shiftResult.Count < 1)
        {
            throw new NotFoundException($"Shift with id {shiftDTO.ShiftId} not found!");
        }
        
        // converting to DAO shift
        Shift sh = new Shift
        {
            ShiftId = shiftResult[0].ShiftId,
            Volunteer = shiftResult[0].Volunteer,
            Event = shiftResult[0].Event,
            Accepted = shiftDTO.Accepted,
            EndTime = shiftDTO.EndTime,
            StartTime = shiftDTO.StartTime
        };

        // attempting to create the new shift in database
        context.Shifts.Update(sh);
        await context.SaveChangesAsync();
        return sh;
    }
}