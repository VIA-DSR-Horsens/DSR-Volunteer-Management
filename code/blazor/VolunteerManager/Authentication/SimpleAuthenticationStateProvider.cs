using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace VolunteerManager.Authentication; 

public class SimpleAuthenticationStateProvider : AuthenticationStateProvider {
    private readonly IAuthManager _authManager;
    
    public SimpleAuthenticationStateProvider(IAuthManager authManager) {
        _authManager = authManager;
        authManager.OnAuthStateChanged += AuthStateChanged;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
        ClaimsPrincipal principal = await _authManager.GetAuthAsync();
        return await Task.FromResult(new AuthenticationState(principal));
    }

    private void AuthStateChanged(ClaimsPrincipal principal) {
        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(principal)
            )
        );
    }
}