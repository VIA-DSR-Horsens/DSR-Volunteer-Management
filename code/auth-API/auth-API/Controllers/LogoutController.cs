using Microsoft.AspNetCore.Mvc;

namespace auth_API.Controllers; 

[ApiController]
[Route("[controller]")]
public class LogoutController : ControllerBase {
	/// <summary>
	/// The method to logout a user
	/// </summary>
	/// <param name="user">The user's cookie</param>
	/// <returns>True, that the user was logged out</returns>
	[HttpPost]
	public async Task<ActionResult<bool>> Logout([FromBody] string cookie) {
		return true;
	}
}