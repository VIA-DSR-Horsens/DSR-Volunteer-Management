using DatabaseEFC.DAO;
using DatabaseEFC.DTO;
using DatabaseEFC.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseEFC.Controllers;

[ApiController]
[Route("[controller]")]
public class ShiftController : ControllerBase
{
    private IShiftDao efc;

    public ShiftController(IShiftDao efc)
    {
        this.efc = efc;
    }

    /// <summary>
    /// Converts DAO type shift to DTO compatible shift
    /// </summary>
    /// <param name="shiftDAO">The DAO shift to convert</param>
    /// <returns>The converted DTO shift</returns>
    private Shift ConvertDaoToDto(Utils.Shift shiftDAO)
    {
        Shift converted = new Shift
        {
            ShiftId = shiftDAO.ShiftId+"",
            EventId = shiftDAO.Event.EventId+"",
            Accepted = shiftDAO.Accepted,
            StartTime = shiftDAO.StartTime,
            EndTime = shiftDAO.EndTime,
            VolunteerId = shiftDAO.Volunteer.VolunteerId+""
        };

        return converted;
    }

    /// <summary>
    /// Create a new shift in the database
    /// </summary>
    /// <param name="shiftDTO">Information about the event to create</param>
    /// <returns>The created event, if successful</returns>
    [HttpPost]
    public async Task<ActionResult<Shift>> Create([FromBody] Shift shiftDTO) {
        try
        {
            var created = await efc.CreateAsync(shiftDTO);
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
    /// Get information about a shift from it's shift id
    /// </summary>
    /// <param name="id">The id of the shift</param>
    /// <returns>Information about a shift, if successful</returns>
    [HttpGet]
    [Route("{id:long}")]
    public async Task<ActionResult<Shift>> GetById([FromRoute] long id)
    {
        try
        {
            var shiftDAO = await efc.GetAsync(id);
            return ConvertDaoToDto(shiftDAO);
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
    /// Updates the shift with new information
    /// </summary>
    /// <param name="shiftDTO">The new information to update in the shift</param>
    /// <returns>The updated shift</returns>
    [HttpPut]
    public async Task<ActionResult<Shift>> Update([FromBody] Shift shiftDTO) {
        try
        {
            var created = await efc.UpdateAsync(shiftDTO);
            return ConvertDaoToDto(created);
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
        catch (NotFoundException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}