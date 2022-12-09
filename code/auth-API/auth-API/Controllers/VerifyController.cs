using auth_API.Logic;
using Microsoft.AspNetCore.Mvc;

namespace auth_API.Controllers; 

[ApiController]
[Route("[controller]")]
public class VerifyController : ControllerBase
{
	private IAuthLogic authLogic;

	public VerifyController(IAuthLogic authLogic)
	{
		this.authLogic = authLogic;
	}
	
	/// <summary>
	/// The method to check does a cookie belong to a user
	/// </summary>
	/// <param name="cookie">The user's cookie</param>
	/// <returns>The UUID of the user whose cookie is it</returns>
	[HttpPost]
	public async Task<ActionResult<long>> VerifyCookie([FromBody] string cookie)
	{
		var uuid = authLogic.VerifyCookie(cookie);
		if (uuid == null)
		{
			return StatusCode(401, "Invalid cookie");
		}

		return uuid;
	}
}