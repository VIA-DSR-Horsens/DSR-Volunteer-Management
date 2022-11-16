using System.ComponentModel.DataAnnotations;

namespace auth_API; 

/// <summary>
/// The user's login information
/// </summary>
public class User {
	/// <summary>
	/// The user's username
	/// </summary>
	[Required]
	public string Username { get; set; }

	/// <summary>
	/// The user's password
	/// </summary>
	[Required]
	public string Password { get; set; }
}