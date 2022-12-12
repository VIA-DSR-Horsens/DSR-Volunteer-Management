using System.ComponentModel.DataAnnotations;

namespace DatabaseEFC.DTO;

/// <summary>
/// Used to communicate via ReST
/// </summary>
public class Manager
{
    /// <summary>
    /// The id assigned to the manager. Sent as string to not lose precision
    /// </summary>
    public string? ManagerId { get; set; }
    
    /// <summary>
    /// The volunteer id of the manager. Sent as string to not lose precision
    /// </summary>
    [Required]
    public string VolunteerId { get; set; }
    /// <summary>
    /// A list of events that the manager is currently managing. Sent as string to not lose precision
    /// </summary>
    public IList<string>? EventsManaged { get; set; }
}