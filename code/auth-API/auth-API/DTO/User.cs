using System.ComponentModel.DataAnnotations;

namespace auth_API.DTO;

/// <summary>
/// The sole purpose of your existence to be received during login phase
/// </summary>
public class User
{
    /// <summary>
    /// The user's email
    /// </summary>
    [Required]
    public string Email { get; set; }
    /// <summary>
    /// The user's password
    /// </summary>
    [Required]
    public string Password { get; set; }
}