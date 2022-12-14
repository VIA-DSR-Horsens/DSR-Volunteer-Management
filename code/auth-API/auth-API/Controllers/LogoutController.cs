using auth_API.Logic;
using Microsoft.AspNetCore.Mvc;

namespace auth_API.Controllers; 

[ApiController]
[Route("[controller]")]
public class LogoutController : ControllerBase
{
	private IAuthLogic authLogic;

	public LogoutController(IAuthLogic authLogic)
	{
		this.authLogic = authLogic;
	}
	
	/// <summary>
	/// The method to logout a user
	/// </summary>
	/// <param name="user">The user's cookie</param>
	/// <returns>True, that the user was logged out</returns>
	[HttpPost]
	public async Task<ActionResult<bool>> Logout([FromBody] string cookie) {
		try {
			if (authLogic.Logout(cookie))
			{
				return true;
			}

			return StatusCode(401, "Invalid cookie");
		} catch (Exception e) {
			return StatusCode(500, e.Message);
		}
		
	}
}