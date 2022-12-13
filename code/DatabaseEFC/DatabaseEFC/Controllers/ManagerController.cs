using DatabaseEFC.DAO;
using DatabaseEFC.DTO;
using DatabaseEFC.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseEFC.Controllers;

[ApiController]
[Route("[controller]")]
public class ManagerController : ControllerBase
{
    private IUserDao efc;

    public ManagerController(IUserDao efc)
    {
        this.efc = efc;
    }

    /// <summary>
    /// Converts DAO type manager to DTO compatible manager
    /// </summary>
    /// <param name="managerDAO">The DAO manager to convert</param>
    /// <returns>The converted DTO manager</returns>
    private Manager ConvertDaoToDto(Utils.Manager managerDAO)
    {
        Manager converted = new Manager
        {
            VolunteerId = managerDAO.Volunteer.VolunteerId+"",
            ManagerId = managerDAO.ManagerId+"",
            EventsManaged = new List<string>()
        };

        if (managerDAO.EventsManaged == null)
            return converted;
        
        foreach (var e in managerDAO.EventsManaged)
        {
            converted.EventsManaged.Add(e.EventId+"");
        }

        return converted;
    }

    /// <summary>
    /// Make a volunteer manager
    /// </summary>
    /// <param name="managerDTO">Information about the manager to create</param>
    /// <returns>The created manager, if successful</returns>
    [HttpPost]
    public async Task<ActionResult<Manager>> Create([FromBody] Manager manager) {
        try
        {
            var created = await efc.CreateAsync(manager);
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
    /// Get information about a manager from it's id
    /// </summary>
    /// <param name="id">The id of the manager</param>
    /// <returns>Information about a manager, if successful</returns>
    [HttpGet]
    [Route("{id:long}")]
    public async Task<ActionResult<Manager>> GetById([FromRoute] long id)
    {
        try
        {
            var managerDAO = await efc.GetManagerAsync(id);
            return ConvertDaoToDto(managerDAO);
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
    /// Get information about a manager from it's volunteer id
    /// </summary>
    /// <param name="id">The id of the manager</param>
    /// <returns>Information about a manager, if successful</returns>
    [HttpGet]
    [Route("Volunteer/{id:long}")]
    public async Task<ActionResult<Manager>> GetByVolunteer([FromRoute] long id)
    {
        try
        {
            var manager = await efc.GetManagerByVolunteerAsync(id);
            var converted = ConvertDaoToDto(manager);
            return converted;
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
            Program.PrintError(e);
            return StatusCode(500, e.Message);
        }
    }
    
    /// <summary>
    /// Removes the volunteer from manager role via manager id
    /// </summary>
    /// <param name="id">The id of the manager</param>
    /// <returns>true, if successful</returns>
    [HttpDelete]
    [Route("{id:long}")]
    public async Task<ActionResult<bool>> DeleteById([FromRoute] long id)
    {
        try
        {
            await efc.DeleteManagerByIdAsync(id);
            return true;
        }
        catch (DbUpdateException e)
        {
            Program.PrintError(e);
            return StatusCode(500, "Error while saving data to database!");
        }
        catch (MinimumRequirementsNotMetException e)
        {
            return StatusCode(409, e.Message);
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
    /// Removes the volunteer from manager role via volunteer id
    /// </summary>
    /// <param name="id">The id of the volunteer</param>
    /// <returns>true, if successful</returns>
    [HttpDelete]
    [Route("Volunteer/{id:long}")]
    public async Task<ActionResult<bool>> DeleteByVolunteer([FromRoute] long id)
    {
        try
        {
            await efc.DeleteManagerByVolunteerAsync(id);
            return true;
        }
        catch (DbUpdateException e)
        {
            Program.PrintError(e);
            return StatusCode(500, "Error while saving data to database!");
        }
        catch (MinimumRequirementsNotMetException e)
        {
            return StatusCode(409, e.Message);
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