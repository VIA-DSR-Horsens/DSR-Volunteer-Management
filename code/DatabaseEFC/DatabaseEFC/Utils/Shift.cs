using System.ComponentModel.DataAnnotations;

namespace DatabaseEFC.Utils;

/// <summary>
/// The shift class used primarily for database
/// </summary>
public class Shift
{
    /// <summary>
    /// The primary key in the database
    /// </summary>
    [Key]
    public long ShiftId { get; set; }

    /// <summary>
    /// The volunteer whose shift it is
    /// </summary>
    [Required]
    public Volunteer Volunteer { get; set; }

    /// <summary>
    /// The event which the shift belongs to
    /// </summary>
    [Required]
    public Event Event { get; set; }

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
}