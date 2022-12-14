namespace auth_API.DAO; 

/// <summary>
/// Interface to allow the program to save, load and update users in database
/// </summary>
public interface IUserDao {
    /// <summary>
    /// Creates a new user in the database
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <returns>New user, which was made</returns>
    Task<User> CreateAsync(DTO.User user);
    /// <summary>
    /// Get an user from the database from their email
    /// </summary>
    /// <param name="email">The email of the user</param>
    /// <returns>The user who matches</returns>
    Task<User> GetAsync(string email);
    /// <summary>
    /// Update user's data in the database
    /// </summary>
    /// <param name="uuid">The user's UUID</param>
    /// <param name="updatedData">The new data of the user</param>
    /// <returns>Updated user data</returns>
    Task<User> UpdateAsync(long uuid, DTO.User updatedData);
}