package dk.dsrhorsens.volunteers.service;

import java.util.HashMap;

/**
 * Cache on the java server to not make repeated requests to the authentication server
 */
public abstract class AuthenticationCache {
    /**
     * Saves valid authentication cookies to user UUID
     */
    private static final HashMap<String, Long> cookieToUuid = new HashMap<>();

    /**
     * Saves a valid authentication cookie to cache
     * @param cookie The authentication cookie
     * @param uuid The user's UUID to which it belongs
     */
    public static void saveAuthenticationCookie(String cookie, long uuid) {
        cookieToUuid.put(cookie, uuid);
    }

    /**
     * Removes an invalid authentication cookie from cache
     * @param cookie The authentication cookie to remove
     */
    public static void invalidateAuthenticationCookie(String cookie) {
        cookieToUuid.remove(cookie);
    }

    /**
     * Gets a saved UUID from the authentication cookie cache, if it exists
     * @param cookie The authentication cookie to look for in cache
     * @return The UUID of the user which the authentication cookie belongs to
     */
    public static Long getUuidFromAuthenticationCookie(String cookie) {
        if (cookieToUuid.containsKey(cookie)) {
            return cookieToUuid.get(cookie);
        }
        return null;
    }
}
