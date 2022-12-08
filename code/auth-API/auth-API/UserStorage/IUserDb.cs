namespace auth_API.UserStorage;

/// <summary>
/// A simple database which stores users
/// </summary>
public interface IUserDb
{
    /// <summary>
    /// Get a user from the database
    /// </summary>
    /// <param name="email">The email of the user</param>
    /// <returns>The user, if found</returns>
    public User? GetUser(string email);
}