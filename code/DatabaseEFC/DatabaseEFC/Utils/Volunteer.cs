using System.ComponentModel.DataAnnotations;

namespace DatabaseEFC.Utils;

/// <summary>
/// Volunteer class used for database
/// </summary>
public class Volunteer
{
    /// <summary>
    /// The primary key of volunteer
    /// </summary>
    [Key]
    public long VolunteerId { get; set; }

    /// <summary>
    /// The full name of volunteer
    /// </summary>
    [Required]
    public string FullName { get; set; } = null!;

    /// <summary>
    /// The e-mail of volunteer
    /// </summary>
    [Required]
    public string Email { get; set; } = null!;

    /// <summary>
    /// The amount shifts the volunteer has taken
    /// </summary>
    [Required]
    public long ShiftsTaken { get; set; }
    
    /// <summary>
    /// The overall rating of the volunteer
    /// </summary>
    [Required]
    public long Rating { get; set; }

    /// <summary>
    /// The shifts volunteer is currently assigned to
    /// </summary>
    public ICollection<Shift>? Shifts { get; set; }
}