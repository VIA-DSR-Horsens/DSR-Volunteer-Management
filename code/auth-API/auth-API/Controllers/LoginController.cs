using Microsoft.AspNetCore.Mvc;

namespace auth_API.Controllers; 

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase {
	/// <summary>
	/// The method to login a user
	/// </summary>
	/// <param name="credentials">The user's credentials</param>
	/// <returns>The login cookie used for future requests</returns>
	[HttpPost]
	public async Task<ActionResult<string>> Login([FromBody] User credentials) {
		var r = new Random();
		return r.NextInt64()+""; // the returned cookie
	}
}