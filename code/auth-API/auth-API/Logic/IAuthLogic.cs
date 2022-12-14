namespace auth_API.Logic;

/// <summary>
/// The authentication logic on the server
/// </summary>
public interface IAuthLogic
{
    /// <summary>
    /// Attempts to log the user in
    /// </summary>
    /// <returns>The cookie of the logged in user, if successful</returns>
    public Task<string?> LoginAsync(string email, string password);
    /// <summary>
    /// Attempt to log the user out via provided cookie
    /// </summary>
    /// <param name="cookie">The login cookie of the user</param>
    /// <returns>True if successful</returns>
    public bool Logout(string cookie);
}