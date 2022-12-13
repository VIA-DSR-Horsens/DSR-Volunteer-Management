using DatabaseEFC.Utils;

namespace DatabaseEFC.DAO;

/// <summary>
/// Used to execute queries related to volunteers, managers and administrators
/// </summary>
public interface IUserDao
{
    
    /// <summary>
    /// Creates a new volunteer in the database
    /// </summary>
    /// <param name="user">The volunteer to create</param>
    /// <returns>Newly created volunteer</returns>
    Task<Volunteer> CreateAsync(DTO.Volunteer user);
    
    /// <summary>
    /// Creates a new manager in the database
    /// </summary>
    /// <param name="user">The manager to create</param>
    /// <returns>Newly created manager</returns>
    Task<Manager> CreateAsync(DTO.Manager user);
    
    /// <summary>
    /// Creates a new administrator in the database
    /// </summary>
    /// <param name="user">The administrator to create</param>
    /// <returns>Newly created administrator</returns>
    Task<Administrator> CreateAsync(DTO.Administrator user);
    
    /// <summary>
    /// Get the volunteer based off id
    /// </summary>
    /// <param name="volunteerId">The volunteer's id</param>
    /// <returns>The volunteer</returns>
    Task<Volunteer> GetVolunteerAsync(long volunteerId);
    /// <summary>
    /// Get the manager based off id
    /// </summary>
    /// <param name="managerId">The manager's id</param>
    /// <returns>The manager</returns>
    Task<Manager> GetManagerAsync(long managerId);
    /// <summary>
    /// Get manager by volunteer id
    /// </summary>
    /// <param name="volunteerId">The volunteer id who might be a manager</param>
    /// <returns>The manager</returns>
    Task<Manager> GetManagerByVolunteerAsync(long volunteerId);
    /// <summary>
    /// Get the administrator based off id
    /// </summary>
    /// <param name="administratorId">The administrator's id</param>
    /// <returns>The administrator</returns>
    Task<Administrator> GetAdministratorAsync(long administratorId);

    /// <summary>
    /// Get the administrator by volunteer id
    /// </summary>
    /// <param name="volunteerId">The volunteer id who might be an administrator</param>
    /// <returns>The administrator</returns>
    Task<Administrator> GetAdministratorByVolunteerAsync(long volunteerId);

    /// <summary>
    /// Removes the volunteer from the manager role
    /// </summary>
    /// <param name="managerId">The manager id to remove</param>
    /// <returns>Completed task</returns>
    Task DeleteManagerByIdAsync(long managerId);
    /// <summary>
    /// Removes the volunteer from the administrator role
    /// </summary>
    /// <param name="administratorId">The administrator id to remove</param>
    /// <returns>Completed task</returns>
    Task DeleteAdministratorByIdAsync(long administratorId);
    /// <summary>
    /// Removes the volunteer from the manager role (Will also remove from administrator!)
    /// </summary>
    /// <param name="volunteerId">The volunteer id to remove from manager position</param>
    /// <param name="providedManager">Optionally provide the manager object</param>
    /// <returns>Completed task</returns>
    Task DeleteManagerByVolunteerAsync(long volunteerId, Manager? providedManager = null);
    /// <summary>
    /// Removes the volunteer from the administrator role
    /// </summary>
    /// <param name="volunteerId">The volunteer id to remove from administrator position</param>
    /// <returns>Completed task</returns>
    Task DeleteAdministratorByVolunteerAsync(long volunteerId);
}