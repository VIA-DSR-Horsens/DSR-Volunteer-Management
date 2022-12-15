using auth_API.DAO;
using auth_API.Logic;
using Microsoft.AspNetCore.Mvc;

namespace auth_API.Controllers; 

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
	private readonly IUserDao userLogic;
	private readonly IAuthLogic authLogic;

	public UserController(IUserDao userLogic, IAuthLogic authLogic)
	{
		this.userLogic = userLogic;
		this.authLogic = authLogic;
	}
	
	/// <summary>
	/// The method to create a new user in external server
	/// </summary>
	/// <param name="credentials">The user's credentials</param>
	/// <returns>The login cookie used for future requests</returns>
	[HttpPost]
	public async Task<ActionResult<string>> SignUp([FromBody] DTO.User credentials) {
		try {
			var newUser = await userLogic.CreateAsync(credentials);
			var loginCookie = await authLogic.LoginAsync(newUser.Email, newUser.Password);
			if (loginCookie == null)
			{
				return StatusCode(401, "Invalid email or password");
			}

			return loginCookie;
		} catch (Exception e) {
			return StatusCode(500, e.Message);
		}
	}
}