using System.ComponentModel.DataAnnotations;

namespace DatabaseEFC.DTO;

/// <summary>
/// Used to communicate via ReST
/// </summary>
public class Administrator
{
    /// <summary>
    /// The id assigned to the administrator
    /// </summary>
    public long? AdministratorId { get; set; }
    
    /// <summary>
    /// The manager id of the administrator
    /// </summary>
    [Required]
    public long ManagerId { get; set; }
}