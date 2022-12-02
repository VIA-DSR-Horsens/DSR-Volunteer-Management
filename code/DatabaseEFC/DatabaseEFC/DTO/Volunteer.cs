using System.ComponentModel.DataAnnotations;

namespace DatabaseEFC.DTO;

/// <summary>
/// Used to communicate via ReST
/// </summary>
public class Volunteer
{
    /// <summary>
    /// The id assigned to the volunteer
    /// </summary>
    public long? VolunteerId { get; init; }
    
    /// <summary>
    /// The volunteer's full name
    /// </summary>
    [Required]
    public string FullName { get; set; } = null!;

    /// <summary>
    /// The volunteer's e-mail
    /// </summary>
    [Required]
    public string Email { get; set; } = null!;

    /// <summary>
    /// The amount of shifts the volunteer has already taken
    /// </summary>
    [Required]
    public long ShiftsTaken { get; set; }

    /// <summary>
    /// Shifts that are currently assigned to the volunteer
    /// </summary>
    public ICollection<long>? Shifts { get; set; }

    /// <summary>
    /// The overall rating of the volunteer
    /// </summary>
    [Required]
    public long Rating { get; set; }
}