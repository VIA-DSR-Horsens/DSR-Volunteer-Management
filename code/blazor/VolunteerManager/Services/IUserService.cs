using VolunteerManager.Models;

namespace VolunteerManager.Services;

/// <summary>
/// Service to manage information about a user
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Get currently logged in user's information from the server
    /// </summary>
    /// <param name="authCookie">The authentication cookie to use</param>
    /// <returns>The logged in user's info, which is stored on the server</returns>
    public Task<User> GetLoggedUserAsync(string authCookie);
}