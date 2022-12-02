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
    /// Get the administrator based off id
    /// </summary>
    /// <param name="administratorId">The administrator's id</param>
    /// <returns>The administrator</returns>
    Task<Administrator> GetAdministratorAsync(long administratorId);

    /// <summary>
    /// Update volunteer data
    /// </summary>
    /// <param name="user">The new data to overwrite with</param>
    /// <returns>The new volunteer data</returns>
    Task<Volunteer> UpdateAsync(Volunteer user);
}