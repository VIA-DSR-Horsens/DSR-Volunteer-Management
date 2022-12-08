using auth_API.Logic;
using Microsoft.AspNetCore.Mvc;

namespace auth_API.Controllers; 

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
	private IAuthLogic authLogic;

	public LoginController(IAuthLogic authLogic)
	{
		this.authLogic = authLogic;
	}
	
	/// <summary>
	/// The method to login a user
	/// </summary>
	/// <param name="credentials">The user's credentials</param>
	/// <returns>The login cookie used for future requests</returns>
	[HttpPost]
	public async Task<ActionResult<string>> Login([FromBody] User credentials)
	{
		var loginCookie = authLogic.Login(credentials.Email, credentials.Password);
		if (loginCookie == null)
		{
			return StatusCode(401, "Invalid email or password");
		}

		return loginCookie;
	}
}