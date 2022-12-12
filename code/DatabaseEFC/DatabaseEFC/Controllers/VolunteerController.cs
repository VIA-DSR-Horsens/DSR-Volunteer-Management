using DatabaseEFC.DAO;
using DatabaseEFC.DTO;
using DatabaseEFC.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseEFC.Controllers;

[ApiController]
[Route("[controller]")]
public class VolunteerController : ControllerBase
{
    private IUserDao efc;

    public VolunteerController(IUserDao efc)
    {
        this.efc = efc;
    }

    /// <summary>
    /// Creates a new volunteer in the database
    /// </summary>
    /// <param name="volunteer">The volunteer to create</param>
    /// <returns>Created volunteer, if successful</returns>
    [HttpPost]
    public async Task<ActionResult<Volunteer>> Create([FromBody] Volunteer volunteer) {
        try
        {
            var created = await efc.CreateAsync(volunteer);

            // converts the response into DTO
            Volunteer converted = new Volunteer
            {
                VolunteerId = created.VolunteerId+"",
                FullName = created.FullName,
                Email = created.Email,
                Rating = created.Rating+"",
                ShiftsTaken = created.ShiftsTaken+""
            };
            return converted;
        }
        catch (InvalidDataException e)
        {
            return StatusCode(400, e.Message);
        }
        catch (DbUpdateException e)
        {
            Program.PrintError(e);
            return StatusCode(500, "Error while saving data to database!");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    /// Get information about a volunteer from their UUID
    /// </summary>
    /// <param name="id">The volunteer's UUID</param>
    /// <returns>The information about the volunteer, if successful</returns>
    [HttpGet]
    [Route("{id:long}")]
    public async Task<ActionResult<Volunteer>> GetById([FromRoute] long id)
    {
        try
        {
            var volunteer = await efc.GetVolunteerAsync(id);
            Volunteer volunteerDTO = new Volunteer
            {
                VolunteerId = volunteer.VolunteerId+"",
                Email = volunteer.Email,
                FullName = volunteer.FullName,
                Rating = volunteer.Rating+"",
                ShiftsTaken = volunteer.ShiftsTaken+"",
                Shifts = new List<string>()
            };
            if (volunteer.Shifts != null)
            {
                foreach (var sh in volunteer.Shifts)
                {
                    volunteerDTO.Shifts.Add(sh.ShiftId+"");
                }
            }

            return volunteerDTO;
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
    /// Gets the shifts of the volunteer
    /// </summary>
    /// <param name="id">The volunteer id whose shifts to get</param>
    /// <returns>A list of shifts</returns>
    [HttpGet]
    [Route("{id:long}/shifts")]
    public async Task<ActionResult<IList<Shift>>> GetShifts([FromRoute] long id)
    {
        try
        {
            var volunteer = await efc.GetVolunteerAsync(id);
            if (volunteer.Shifts == null)
            {
                return new List<Shift>();
            }

            var shifts = new List<Shift>();
            foreach (var sh in volunteer.Shifts)
            {
                var shiftDTO = new Shift
                {
                    Accepted = sh.Accepted,
                    EndTime = sh.EndTime,
                    EventId = sh.Event.EventId+"",
                    ShiftId = sh.ShiftId+"",
                    StartTime = sh.StartTime,
                    VolunteerId = sh.Volunteer.VolunteerId+""
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