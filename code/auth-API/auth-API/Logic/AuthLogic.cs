using auth_API.UserStorage;

namespace auth_API.Logic;

public class AuthLogic : IAuthLogic
{
    private IUserDb userDb;
    /// <summary>
    /// Currently saved cookies. Key authorization cookie, value is UUID
    /// </summary>
    private Dictionary<string, long> cookieToUuid;
    /// <summary>
    /// Currently saved cookies. Key UUID cookie, value is cookie
    /// </summary>
    private Dictionary<long, string> uuidToCookie;

    public AuthLogic(IUserDb userDb)
    {
        this.userDb = userDb;
        cookieToUuid = new Dictionary<string, long>();
        uuidToCookie = new Dictionary<long, string>();
    }

    public long? VerifyCookie(string cookie)
    {
        if (!cookieToUuid.ContainsKey(cookie))
        {
            return null;
        }

        return cookieToUuid[cookie];
    }
    
    public string? Login(string email, string password)
    {
        var loggedUser = userDb.GetUser(email);
        if (loggedUser == null || !loggedUser.Password.Equals(password))
        {
            return null;
        }

        // password matches
        // checking if auth cookie already exists
        if (uuidToCookie.ContainsKey(loggedUser.Uuid))
        {
            return uuidToCookie[loggedUser.Uuid];
        }
        
        // creating new cookie
        var r = new Random();
        string cookie = "" + r.NextInt64();
        while (cookieToUuid.ContainsKey(cookie))
        {
            cookie = "" + r.NextInt64();
        }
        
        cookieToUuid.Add(cookie, loggedUser.Uuid);
        uuidToCookie.Add(loggedUser.Uuid, cookie);
        return cookie;
    }

    public bool Logout(string authCookie)
    {
        if (!cookieToUuid.ContainsKey(authCookie))
        {
            return false;
        }

        long uuid = cookieToUuid[authCookie];
        cookieToUuid.Remove(authCookie);
        uuidToCookie.Remove(uuid);
        
        return true;
    }
}