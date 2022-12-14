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
    private IEventDao efc;
    private IShiftDao shiftEfc;

    public EventController(IEventDao efc, IShiftDao shiftEfc)
    {
        this.efc = efc;
        this.shiftEfc = shiftEfc;
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
            EventId = eventDAO.EventId+"",
            Date = eventDAO.Date,
            EventName = eventDAO.EventName,
            Managers = new List<string>()
        };
        if (eventDAO.Location != null)
            converted.Location = eventDAO.Location;
        if (eventDAO.StartTime != null)
            converted.StartTime = eventDAO.StartTime;
        if (eventDAO.EndTime != null)
            converted.EndTime = eventDAO.EndTime;
        foreach (var m in eventDAO.Managers)
        {
            converted.Managers.Add(m.ManagerId+"");
        }

        if (eventDAO.Shifts != null)
        {
            converted.Shifts = new List<string>();
            foreach (var s in eventDAO.Shifts)
            {
                converted.Shifts.Add(s.ShiftId+"");
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
            var created = await efc.CreateAsync(eventDTO);
            return ConvertDaoToDto(created);
        }
        catch (DbUpdateException e)
        {
            Program.PrintError(e);
            return StatusCode(500, "Error while saving data to database!");
        }
        catch (InvalidDataException e)
        {
            return StatusCode(400, e.Message);
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
            var eventDAO = await efc.GetByIdAsync(id);
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

    /// <summary>
    /// Gets a list of all the events in the database
    /// </summary>
    /// <returns>A list of events</returns>
    [HttpGet]
    public async Task<ActionResult<List<Event>>> GetAllEvents()
    {
        try
        {
            var eventsDAO = await efc.GetAsync();
            List<Event> eventsDTO = new List<Event>();

            foreach (var e in eventsDAO)
            {
                eventsDTO.Add(ConvertDaoToDto(e));
            }

            return eventsDTO;
        }
        catch (Exception e)
        {
            Program.PrintError(e);
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Gets the shifts of the event
    /// </summary>
    /// <param name="id">The event id whose shifts to get</param>
    /// <returns>A list of shifts</returns>
    [HttpGet]
    [Route("{id:long}/Shifts")]
    public async Task<ActionResult<IList<Shift>>> GetShifts([FromRoute] long id)
    {
        try
        {
            var eventDAO = await efc.GetByIdAsync(id);
            if (eventDAO.Shifts == null)
            {
                return new List<Shift>();
            }

            var shifts = new List<Shift>();
            foreach (var sh in eventDAO.Shifts)
            {
                // we need to call the method to get more in depth information about the shift
                var fullShift = await shiftEfc.GetAsync(sh.ShiftId);
                var shiftDTO = new Shift
                {
                    Accepted = fullShift.Accepted,
                    EndTime = fullShift.EndTime,
                    EventId = fullShift.Event.EventId + "",
                    ShiftId = fullShift.ShiftId + "",
                    StartTime = fullShift.StartTime,
                    VolunteerId = fullShift.Volunteer.VolunteerId + ""
                };
                shifts.Add(shiftDTO);
            }

            return shifts;
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