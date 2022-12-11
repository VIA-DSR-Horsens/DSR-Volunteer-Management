using System.ComponentModel.DataAnnotations;

namespace DatabaseEFC.DTO;

/// <summary>
/// Used to communicate via ReST
/// </summary>
public class Shift
{
    /// <summary>
    /// The id assigned to this shift
    /// </summary>
    public long? ShiftId { get; set; }

    /// <summary>
    /// The id of volunteer, who goes to the shift
    /// </summary>
    [Required]
    public long VolunteerId { get; set; }

    /// <summary>
    /// The id of event where shift is taking place
    /// </summary>
    [Required]
    public long EventId { get; set; }

    /// <summary>
    /// The start time of the shift
    /// </summary>
    [Required]
    public string StartTime { get; set; }
    
    /// <summary>
    /// The end time of the shift
    /// </summary>
    [Required]
    public string EndTime { get; set; }
    
    /// <summary>
    /// Whether the volunteer's shift has been accepted by the event
    /// </summary>
    public bool? Accepted { get; set; }
}