using Grpc.Core;
using VolunteerManager.Exceptions;
using VolunteerManager.Models;
using VolunteerManager.UserProto;

namespace VolunteerManager.Services;

public class UserServiceImpl : UserService.UserServiceClient, IUserService
{
    /// <summary>
    /// Gets the logged in user from Java server using gRPC
    /// </summary>
    /// <param name="authCookie">The authentication cookie from login</param>
    /// <returns>The logged in user</returns>
    public async Task<User> GetLoggedUserAsync(string authCookie)
    {
        var request = new AuthenticationRequest { AuthenticationCookie = authCookie };
        var response = await authenticateAsync(request);
        if (response == null)
        {
            // Either connection failed or nothing returned (invalid cookie)
            throw new ConnectionFailedException("Didn't receive a response from DSR server while logging in!");
        }

        // converting gRPC response to a user object
        var converted = new User
        {
            AuthCookie = authCookie,
            Email = response.Email,
            FullName = response.FullName,
            IsAdministrator = response.IsAdministrator,
            IsManager = response.IsManager,
            Rating = response.Rating,
            ShiftsTaken = response.ShiftsTaken,
            Shifts = response.Shifts,
            EventsManaged = response.EventsManaged,
            RequestedShifts = response.RequestedShifts
        };

        return converted;
    }
}