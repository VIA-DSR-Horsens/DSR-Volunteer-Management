using Microsoft.AspNetCore.Mvc;

namespace auth_API.Controllers; 

[ApiController]
[Route("[controller]")]
public class AuthenticateController : ControllerBase {
	/// <summary>
	/// The method to verify already logged in cookie
	/// </summary>
	/// <param name="cookie"></param>
	/// <returns></returns>
	[HttpPost]
	public async Task<ActionResult<bool>> VerifyCookie([FromBody] string cookie) {
		return true;
	}
}