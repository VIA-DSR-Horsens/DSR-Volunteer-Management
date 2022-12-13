using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseEFC.Utils;

/// <summary>
/// The manager class used primarily for database
/// </summary>
public class Manager
{
    /// <summary>
    /// The primary key in the database
    /// </summary>
    [Key]
    public long ManagerId { get; set; }

    /// <summary>
    /// The volunteer which the manager extends
    /// </summary>
    [Required]
    public Volunteer Volunteer { get; set; } = null!;

    /// <summary>
    /// Optional list of events which the manager is currently managing
    /// </summary>
    public IList<Event>? EventsManaged { get; set; }
}