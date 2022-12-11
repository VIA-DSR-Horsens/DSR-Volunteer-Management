using System.Security.Claims;

namespace VolunteerManager.Authentication; 

public interface IAuthManager {
    /// <summary>
    /// Attempts to log the user in and get their user info
    /// </summary>
    /// <param name="email">The user's email</param>
    /// <param name="password">The user's password</param>
    /// <returns>Completed task</returns>
    public Task LoginAsync(string email, string password);
    /// <summary>
    /// Attempts to log the user out of the system by using the saved authentication cookie from logging in
    /// </summary>
    /// <returns>Completed task</returns>
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}