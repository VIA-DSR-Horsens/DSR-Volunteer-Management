using System.ComponentModel.DataAnnotations;

namespace DatabaseEFC.DTO;

/// <summary>
/// Used to communicate via ReST
/// </summary>
public class Shift
{
    /// <summary>
    /// The id assigned to this shift. Sent as string to not lose precision
    /// </summary>
    public string? ShiftId { get; set; }

    /// <summary>
    /// The id of volunteer, who goes to the shift. Sent as string to not lose precision
    /// </summary>
    [Required]
    public string VolunteerId { get; set; }

    /// <summary>
    /// The id of event where shift is taking place. Sent as string to not lose precision
    /// </summary>
    [Required]
    public string EventId { get; set; }

    /// <summary>
    /// The start time of the shift
    /// </summary>
    [Required]
    public string StartTime { get; set; } = null!;

    /// <summary>
    /// The end time of the shift
    /// </summary>
    [Required]
    public string EndTime { get; set; } = null!;
    
    /// <summary>
    /// Whether the volunteer's shift has been accepted by the event
    /// </summary>
    [Required]
    public bool Accepted { get; set; }
}