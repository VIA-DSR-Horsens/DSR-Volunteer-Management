using System.ComponentModel.DataAnnotations;

namespace DatabaseEFC.DTO;

/// <summary>
/// Used to communicate via ReST
/// </summary>
public class Administrator
{
    /// <summary>
    /// The id assigned to the administrator. Sent as string to not lose precision
    /// </summary>
    public string? AdministratorId { get; set; }
    
    /// <summary>
    /// The volunteer id of the administrator. Sent as string to not lose precision
    /// </summary>
    [Required]
    public string VolunteerId { get; set; }
    /// <summary>
    /// The id of the volunteer's manager role. Sent as string to not lose precision
    /// </summary>
    public string? ManagerId { get; set; }
}