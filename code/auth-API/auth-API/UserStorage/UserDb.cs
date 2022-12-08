namespace auth_API.UserStorage;

public class UserDb : IUserDb
{
    /// <summary>
    /// Saved users in database. Key is email and value is saved user
    /// </summary>
    private Dictionary<string, User> users;

    public UserDb()
    {
        users = new Dictionary<string, User>();
        users.Add("315210@via.dk", new User {Email = "315210@via.dk", Password = "Password", Uuid = 315210});
    }

    public User? GetUser(string email)
    {
        return users.ContainsKey(email) ? users[email] : null;
    }
}