using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.JSInterop;
using VolunteerManager.Models;
using VolunteerManager.Services;

namespace VolunteerManager.Authentication;

public class AuthManagerImpl : IAuthManager
{
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!; // assigning to null! to suppress null warning.
    private readonly IUserService userService;
    private readonly IJSRuntime jsRuntime;
    private ClaimsPrincipal cachedClaims;
    
    public AuthManagerImpl(IUserService userService, IJSRuntime jsRuntime)
    {
        this.userService = userService;
        this.jsRuntime = jsRuntime;
        this.cachedClaims = CreateClaimsPrincipal(null); // create a new ClaimsPrincipal with nothing.
    }
    
    public async Task LoginAsync(string email, string password)
    {
        // Making a HTTP request to get cookie
        using HttpClient httpClient = new HttpClient();
        var loginRequestContent = new StringContent(
            JsonSerializer.Serialize(new DTO.User{Email = email, Password = password}),
            Encoding.UTF8,
            "application/json"
            );
        HttpResponseMessage loginResponse =
            await httpClient.PostAsync("https://localhost:7006/Login", loginRequestContent);
        if (!loginResponse.IsSuccessStatusCode)
        {
            throw new Exception($"An error occoured while logging in! Http Status: {loginResponse.StatusCode}," +
                                $"Message: {loginResponse.ReasonPhrase}");
        }
        
        // successful login, time to get user info from gRPC
        var user = await userService.GetLoggedUserAsync(loginResponse.ReasonPhrase);
        await CacheUserAsync(user);
        ClaimsPrincipal principal = CreateClaimsPrincipal(user); // convert user object to ClaimsPrincipal

        OnAuthStateChanged?.Invoke(principal); // notify interested classes in the change of authentication state
    }

    public async Task LogoutAsync()
    {
        // Making a HTTP request to log out
        // getting the cookie from claims
        string cookie = "";
        foreach (var c in cachedClaims.Claims)
        {
            if (c.Type == ClaimTypes.Name)
            {
                cookie = c.Value;
                break;
            }
        }
        
        // sending the http request
        using HttpClient httpClient = new HttpClient();
        var logoutRequestContent = new StringContent(
            JsonSerializer.Serialize(cookie),
            Encoding.UTF8,
            "application/json"
        );
        HttpResponseMessage logoutResponse =
            await httpClient.PostAsync("https://localhost:7006/Logout", logoutRequestContent);
        if (!logoutResponse.IsSuccessStatusCode)
        {
            throw new Exception($"An error occoured while logging out! Http Status: {logoutResponse.StatusCode}," +
                                $"Message: {logoutResponse.ReasonPhrase}");
        }
        
        // Clearing the user from browser cache
        await ClearUserFromCacheAsync(); // remove the user object from browser cache
        ClaimsPrincipal principal = CreateClaimsPrincipal(null); // create a new ClaimsPrincipal with nothing.
        cachedClaims = principal;
        OnAuthStateChanged?.Invoke(principal); // notify about change in authentication state
    }

    /// <summary>
    /// This method is called by the authentication framework, whenever user credentials are required
    /// </summary>
    /// <returns></returns>
    public async Task<ClaimsPrincipal> GetAuthAsync()
    {
        if (cachedClaims.Identity == null)
            return cachedClaims;
        
        var user =  await GetUserFromCacheAsync(); // retrieve cached user, if any
        ClaimsPrincipal principal = CreateClaimsPrincipal(user); // create ClaimsPrincipal
        cachedClaims = principal;

        return principal;
    }

    /// <summary>
    /// Get the saved user data from browser cache
    /// </summary>
    /// <returns>The user data, if saved</returns>
    private async Task<User?> GetUserFromCacheAsync()
    {
        string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
        if (string.IsNullOrEmpty(userAsJson)) return null;
        User user = JsonSerializer.Deserialize<User>(userAsJson)!;
        return user;
    }

    /// <summary>
    /// Create a new claims principal based off of provided user data
    /// </summary>
    /// <param name="user">The user's data</param>
    /// <returns>Claims principal based off of user's data</returns>
    private static ClaimsPrincipal CreateClaimsPrincipal(User? user)
    {
        if (user != null)
        {
            ClaimsIdentity identity = ConvertUserToClaimsIdentity(user);
            return new ClaimsPrincipal(identity);
        }

        return new ClaimsPrincipal();
    }

    /// <summary>
    /// Cache the user data in browser
    /// </summary>
    /// <param name="user">The user data to cache</param>
    private async Task CacheUserAsync(User user)
    {
        string serialisedData = JsonSerializer.Serialize(user);
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "userInfo", serialisedData);
    }

    /// <summary>
    /// Clear the user data from browser's cache
    /// </summary>
    private async Task ClearUserFromCacheAsync()
    {
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "userInfo", "");
    }

    /// <summary>
    /// Converts the user's data into a claims identity object
    /// </summary>
    /// <param name="user">The user data to convert</param>
    /// <returns>Converted data</returns>
    private static ClaimsIdentity ConvertUserToClaimsIdentity(User user)
    {
        // here we take the information of the User object and convert to Claims
        // this is (probably) the only method, which needs modifying for your own user type
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, user.AuthCookie),
            new Claim("Email", user.Email),
            new Claim("FullName", user.FullName),
            new Claim("ShiftsTaken", user.ShiftsTaken+""),
            new Claim("Rating", user.Rating+""),
            new Claim("Shifts", JsonSerializer.Serialize(user.Shifts)),
            new Claim("EventsManaged", JsonSerializer.Serialize(user.EventsManaged)),
            new Claim("RequestedShifts", JsonSerializer.Serialize(user.RequestedShifts)),
            new Claim("IsManager", user.IsManager.ToString()),
            new Claim("IsAdministrator", user.IsAdministrator.ToString())
        };

        return new ClaimsIdentity(claims, "apiauth_type");
    }
}