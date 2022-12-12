using System.ComponentModel.DataAnnotations;

namespace DatabaseEFC.DTO;

/// <summary>
/// Used to communicate via ReST
/// </summary>
public class Event
{
    /// <summary>
    /// The id assigned to the event. Sent as string to not lose precision
    /// </summary>
    public string? EventId { get; set; }
    
    /// <summary>
    /// The name of the event
    /// </summary>
    [Required]
    public string EventName { get; set; } = null!;

    /// <summary>
    /// The date when event takes place
    /// </summary>
    [Required]
    public string Date { get; set; } = null!;

    /// <summary>
    /// The start time of the event
    /// </summary>
    public string? StartTime { get; set; }
    
    /// <summary>
    /// The end time of the event
    /// </summary>
    public string? EndTime { get; set; }
    
    /// <summary>
    /// The place where event takes place
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// A list of manager ids, who are managing the event. Sent as string to not lose precision
    /// </summary>
    [Required]
    public ICollection<string> Managers { get; set; } = null!;

    /// <summary>
    /// A list of shift ids, which are a part of this event. Sent as string to not lose precision
    /// </summary>
    public ICollection<string>? Shifts { get; set; }
}