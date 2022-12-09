namespace auth_API; 

/// <summary>
/// The user's login information
/// </summary>
public class User {
	/// <summary>
	/// The user's username
	/// </summary>
	public string Email { get; set; }
	
	/// <summary>
	/// Unique user ID which is shared between Java server and Auth Server
	/// </summary>
	public long Uuid { get; init; }

	/// <summary>
	/// The user's password
	/// </summary>
	public string Password { get; set; }
}