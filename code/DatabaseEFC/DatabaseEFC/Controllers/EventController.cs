using DatabaseEFC.DAO;
using DatabaseEFC.DTO;
using DatabaseEFC.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseEFC.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private IEventDao eventEfc;

    public EventController(IEventDao efc)
    {
        eventEfc = efc;
    }

    /// <summary>
    /// Converts DAO type event to DTO compatible event
    /// </summary>
    /// <param name="eventDAO">The DAO event to convert</param>
    /// <returns>The converted DTO event</returns>
    private Event ConvertDaoToDto(Utils.Event eventDAO)
    {
        Event converted = new Event
        {
            EventId = eventDAO.EventId,
            Date = eventDAO.Date,
            EventName = eventDAO.EventName,
            Managers = new List<long>()
        };
        if (eventDAO.Location != null)
            converted.Location = eventDAO.Location;
        if (eventDAO.StartTime != null)
            converted.StartTime = eventDAO.StartTime;
        if (eventDAO.EndTime != null)
            converted.EndTime = eventDAO.EndTime;
        foreach (var m in eventDAO.Managers)
        {
            converted.Managers.Add(m.ManagerId);
        }

        if (eventDAO.Shifts != null)
        {
            converted.Shifts = new List<long>();
            foreach (var s in eventDAO.Shifts)
            {
                converted.Shifts.Add(s.ShiftId);
            }
        }

        return converted;
    }

    /// <summary>
    /// Create a new event in the database
    /// </summary>
    /// <param name="eventDTO">Information about the event to create</param>
    /// <returns>The created event, if successful</returns>
    [HttpPost]
    public async Task<ActionResult<Event>> Create([FromBody] Event eventDTO) {
        try
        {
            var created = await eventEfc.CreateAsync(eventDTO);
            return ConvertDaoToDto(created);
        }
        catch (DbUpdateException e)
        {
            Program.PrintError(e);
            return StatusCode(500, "Error while saving data to database!");
        }
        catch (NotFoundException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Get information about an event from it's event id
    /// </summary>
    /// <param name="id">The id of the event</param>
    /// <returns>Information about an event, if successful</returns>
    [HttpGet]
    [Route("{id:long}")]
    public async Task<ActionResult<Event>> GetById([FromRoute] long id)
    {
        try
        {
            var eventDAO = await eventEfc.GetByIdAsync(id);
            return ConvertDaoToDto(eventDAO);
        }
        catch (NotFoundException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (Exception e)
        {
            Program.PrintError(e);
            return StatusCode(500, e.Message);
        }
    }
}