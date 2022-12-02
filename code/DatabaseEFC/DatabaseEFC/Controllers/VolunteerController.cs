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
    private IUserDao userEfc;

    public VolunteerController(IUserDao efc)
    {
        userEfc = efc;
    }

    [HttpPost]
    public async Task<ActionResult<Volunteer>> Create([FromBody] Volunteer volunteer) {
        try
        {
            var created = await userEfc.CreateAsync(volunteer);

            // converts the response into DTO
            Volunteer converted = new Volunteer
            {
                VolunteerId = created.VolunteerId,
                FullName = created.FullName,
                Email = created.Email,
                Rating = created.Rating,
                ShiftsTaken = created.ShiftsTaken
            };
            return converted;
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

    [HttpGet]
    [Route("{id:long}")]
    public async Task<ActionResult<Volunteer>> GetById([FromRoute] long id)
    {
        try
        {
            var volunteer = await userEfc.GetVolunteerAsync(id);
            Volunteer volunteerDTO = new Volunteer
            {
                VolunteerId = volunteer.VolunteerId,
                Email = volunteer.Email,
                FullName = volunteer.FullName,
                Rating = volunteer.Rating,
                ShiftsTaken = volunteer.ShiftsTaken,
                Shifts = new List<long>()
            };
            if (volunteer.Shifts != null)
            {
                foreach (var sh in volunteer.Shifts)
                {
                    volunteerDTO.Shifts.Add(sh.ShiftId);
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
}