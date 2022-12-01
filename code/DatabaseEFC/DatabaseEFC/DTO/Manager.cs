using System.ComponentModel.DataAnnotations;

namespace DatabaseEFC.DTO;

/// <summary>
/// Used to communicate via ReST
/// </summary>
public class Manager
{
    /// <summary>
    /// The id assigned to the manager
    /// </summary>
    public long? ManagerId { get; set; }
    
    /// <summary>
    /// The volunteer id of the manager
    /// </summary>
    [Required]
    public long VolunteerId { get; set; }
}