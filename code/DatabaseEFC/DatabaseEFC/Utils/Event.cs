using System.ComponentModel.DataAnnotations;

namespace DatabaseEFC.Utils;

/// <summary>
/// The event class used primarily for database
/// </summary>
public class Event
{
    /// <summary>
    /// Primary key for database
    /// </summary>
    [Key]
    public long EventId { get; set; }

    /// <summary>
    /// The event name
    /// </summary>
    [Required]
    public string EventName { get; set; } = null!;

    /// <summary>
    /// The date when event takes place
    /// </summary>
    [Required]
    public string Date { get; set; } = null!;

    /// <summary>
    /// Optional specific time when event starts
    /// </summary>
    public string? StartTime { get; set; }
    /// <summary>
    /// Optional specific time when event ends
    /// </summary>
    public string? EndTime { get; set; }
    /// <summary>
    /// Optional location where event takes place
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Optional shifts which the event has
    /// </summary>
    public ICollection<Shift>? Shifts { get; set; }
    /// <summary>
    /// The managers who are managing the event
    /// </summary>
    [Required]
    public ICollection<Manager> Managers { get; set; } = null!;
}