using DatabaseEFC.DAO;
using DatabaseEFC.DTO;
using DatabaseEFC.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseEFC.Controllers;

[ApiController]
[Route("[controller]")]
public class AdministratorController : ControllerBase
{
    private IUserDao efc;

    public AdministratorController(IUserDao efc)
    {
        this.efc = efc;
    }

    /// <summary>
    /// Converts DAO type administrator to DTO compatible administrator
    /// </summary>
    /// <param name="administratorDAO">The DAO administrator to convert</param>
    /// <returns>The converted DTO administrator</returns>
    private Administrator ConvertDaoToDto(Utils.Administrator administratorDAO)
    {
        Administrator converted = new Administrator
        {
            VolunteerId = administratorDAO.Volunteer.VolunteerId+"",
            AdministratorId = administratorDAO.AdministratorId+""
        };

        return converted;
    }

    /// <summary>
    /// Make a volunteer administrator
    /// </summary>
    /// <param name="administratorDTO">Information about the administrator to create</param>
    /// <returns>The created administrator, if successful</returns>
    [HttpPost]
    public async Task<ActionResult<Administrator>> Create([FromBody] Administrator administrator) {
        try
        {
            var created = await efc.CreateAsync(administrator);
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
    /// Get information about a administrator from it's administrator id
    /// </summary>
    /// <param name="id">The id of the administrator</param>
    /// <returns>Information about a administrator, if successful</returns>
    [HttpGet]
    [Route("{id:long}")]
    public async Task<ActionResult<Administrator>> GetById([FromRoute] long id)
    {
        try
        {
            var administratorDAO = await efc.GetAdministratorAsync(id);
            return ConvertDaoToDto(administratorDAO);
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
    /// Get information about an administrator from it's volunteer id
    /// </summary>
    /// <param name="id">The id of the volunteer</param>
    /// <returns>Information about a administrator, if successful</returns>
    [HttpGet]
    [Route("Volunteer/{id:long}")]
    public async Task<ActionResult<Administrator>> GetByVolunteer([FromRoute] long id)
    {
        try
        {
            var administrator = await efc.GetAdministratorByVolunteerAsync(id);
            return ConvertDaoToDto(administrator);
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
    /// Removes the volunteer from administrator role via administrator id
    /// </summary>
    /// <param name="id">The id of the administrator</param>
    /// <returns>true, if successful</returns>
    [HttpDelete]
    [Route("{id:long}")]
    public async Task<ActionResult<bool>> DeleteById([FromRoute] long id)
    {
        try
        {
            await efc.DeleteAdministratorByIdAsync(id);
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
    /// Removes the volunteer from administrator role via volunteer id
    /// </summary>
    /// <param name="id">The id of the volunteer</param>
    /// <returns>true, if successful</returns>
    [HttpDelete]
    [Route("Volunteer/{id:long}")]
    public async Task<ActionResult<bool>> DeleteByVolunteer([FromRoute] long id)
    {
        try
        {
            await efc.DeleteAdministratorByVolunteerAsync(id);
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