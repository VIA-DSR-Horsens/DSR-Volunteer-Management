namespace auth_API.Logic; 

/// <summary>
/// Singleton class to store saved cookies and UUID from authentication
/// </summary>
public class AuthSingleton {
    /// <summary>
    /// Currently saved cookies. Key authorization cookie, value is UUID
    /// </summary>
    private readonly Dictionary<string, long> cookieToUuid;
    /// <summary>
    /// Currently saved cookies. Key UUID cookie, value is cookie
    /// </summary>
    private readonly Dictionary<long, string> uuidToCookie;

    public AuthSingleton() {
        cookieToUuid = new Dictionary<string, long>();
        uuidToCookie = new Dictionary<long, string>();
    }

    /// <summary>
    /// Handler to notify when a cookie is removed
    /// </summary>
    public delegate void CookieRemovedEventHandler(string cookie, long uuid);
    /// <summary>
    /// Event to subscribe to, to listen when an authentication cookie is removed
    /// </summary>
    public event CookieRemovedEventHandler CookieRemoved;
    
    /// <summary>
    /// Gets user's authentication cookie from their UUID
    /// </summary>
    /// <param name="uuid">The UUID of the user</param>
    /// <returns>Their authentication cookie, if it exists</returns>
    public string? CookieFromUuid(long uuid) {
        return uuidToCookie.ContainsKey(uuid) ? uuidToCookie[uuid] : null;
    }
    
    /// <summary>
    /// Gets user's UUID from their authentication cookie
    /// </summary>
    /// <param name="cookie">The authentication cookie of the user</param>
    /// <returns>Their UUID, if the cookie is valid</returns>
    public long? UuidFromCookie(string cookie) {
        return cookieToUuid.ContainsKey(cookie) ? cookieToUuid[cookie] : null;
    }

    /// <summary>
    /// Saves a new pair of authentication cookie and UUID
    /// </summary>
    /// <param name="cookie">The authentication cookie of the user</param>
    /// <param name="uuid">The UUID of the user</param>
    public void NewCookie(string cookie, long uuid) {
        cookieToUuid.Add(cookie, uuid);
        uuidToCookie.Add(uuid, cookie);
    }

    /// <summary>
    /// Invalidates the authentication cookie
    /// </summary>
    /// <param name="cookie">The cookie to invalidate</param>
    public void RemoveCookie(string cookie) {
        if (!cookieToUuid.ContainsKey(cookie))
            return;

        var uuid = cookieToUuid[cookie];
        cookieToUuid.Remove(cookie);
        uuidToCookie.Remove(uuid);
        
        CookieRemoved.Invoke(cookie, uuid);
    }
}