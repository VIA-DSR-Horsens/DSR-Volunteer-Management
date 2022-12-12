using System.ComponentModel.DataAnnotations;

namespace DatabaseEFC.Utils;

/// <summary>
/// The administrator class used primarily for database
/// </summary>
public class Administrator
{
    /// <summary>
    /// Primary key in the database
    /// </summary>
    [Key]
    public long AdministratorId { get; set; }

    /// <summary>
    /// The volunteer which the administrator extends
    /// </summary>
    [Required]
    public Volunteer Volunteer { get; set; }
}