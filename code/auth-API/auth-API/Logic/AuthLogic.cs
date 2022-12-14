using System.Diagnostics;
using auth_API.AuthenticationProto;
using auth_API.DAO;
using Grpc.Core;

namespace auth_API.Logic;

public class AuthLogic : AuthenticationService.AuthenticationServiceBase, IAuthLogic
{
    /// <summary>
    /// The user database
    /// </summary>
    private readonly IUserDao userDb;
    /// <summary>
    /// The cookie cache
    /// </summary>
    private readonly AuthSingleton storage;

    public AuthLogic(IUserDao userDb, AuthSingleton storage)
    {
        this.userDb = userDb;
        this.storage = storage;
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var loggedUser = await userDb.GetAsync(email);
        if (loggedUser == null || !loggedUser.Password.Equals(password))
        {
            return null;
        }

        // password matches
        // checking if auth cookie already exists
        var cookie = storage.CookieFromUuid(loggedUser.Uuid);
        if (cookie != null)
            return cookie;

        // creating new cookie
        var r = new Random();
        cookie = "" + r.NextInt64();
        while (storage.UuidFromCookie(cookie) != null)
        {
            cookie = "" + r.NextInt64();
        }
        
        storage.NewCookie(cookie, loggedUser.Uuid);
        return cookie;
    }

    public bool Logout(string authCookie)
    {
        if (storage.UuidFromCookie(authCookie) == null)
        {
            return false;
        }

        storage.RemoveCookie(authCookie);
        return true;
    }
    
    // Proto stuff
    // Handles verifying cookies and returns the user's UUID from the matching cookie
    public override Task<VerificationResponse> verifyCookie(CookieVerification cookie, ServerCallContext context) {
        var uuid = storage.UuidFromCookie(cookie.AuthenticationCookie);
        if (uuid == null) {
            //cookie not found
            return Task.FromResult(new VerificationResponse {
                AuthenticationCookie = cookie.AuthenticationCookie,
                Uuid = 0,
                ValidCookie = false
            });
        }
        
        // valid cookie
        return Task.FromResult(new VerificationResponse {
            AuthenticationCookie = cookie.AuthenticationCookie,
            Uuid = uuid.Value,
            ValidCookie = true
        });
    }

    public override async Task startListening(EmptyMessage request, IServerStreamWriter<InvalidatedCookie> responseStream, ServerCallContext context) {
        async void OnStorageOnCookieRemoved(string authCookie, long uuid) {
            if (context.CancellationToken.IsCancellationRequested) {
                storage.CookieRemoved -= OnStorageOnCookieRemoved;
            }

            await responseStream.WriteAsync(new InvalidatedCookie { AuthenticationCookie = authCookie, Uuid = uuid });
        }

        storage.CookieRemoved += OnStorageOnCookieRemoved;
        
        while (true) {
            await new Task(() => {
                Thread.Sleep(500);
            });
            if (context.CancellationToken.IsCancellationRequested) {
                return;
            }
        }
    }
}