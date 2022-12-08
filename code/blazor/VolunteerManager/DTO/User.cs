using System.ComponentModel.DataAnnotations;

namespace VolunteerManager.DTO;

/// <summary>
/// The sole purpose of your existence is to be sent to authentication server as a JSON string
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