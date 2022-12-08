using VolunteerManager.Models;

namespace VolunteerManager.Services;

public class UserServiceImpl : IUserService
{
    /// <summary>
    /// Gets the logged in user from Java server using gRPC
    /// </summary>
    /// <param name="authCookie">The authentication cookie from login</param>
    /// <returns>The logged in user</returns>
    public async Task<User> GetLoggedUserAsync(string authCookie)
    {
        
    }
}