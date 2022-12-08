using System.Security.Claims;

namespace VolunteerManager.Authentication; 

public interface IAuthManager {
    public Task LoginAsync(string email, string password);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}