using System.ComponentModel.DataAnnotations;

namespace DatabaseEFC.DTO;

/// <summary>
/// Used to communicate via ReST
/// </summary>
public class Volunteer
{
    /// <summary>
    /// The UUID assigned to the volunteer. Sent as string to not lose precision
    /// </summary>
    [Required]
    public string VolunteerId { get; init; }
    
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
    /// The amount of shifts the volunteer has already taken. Sent as string to not lose precision (64bit)
    /// </summary>
    public string? ShiftsTaken { get; set; }

    /// <summary>
    /// Shifts that are currently assigned to the volunteer. Sent as string to not lose precision
    /// </summary>
    public ICollection<string>? Shifts { get; set; }

    /// <summary>
    /// The overall rating of the volunteer. Sent as string to not lose precision (64bit int)
    /// </summary>
    public string? Rating { get; set; }
}