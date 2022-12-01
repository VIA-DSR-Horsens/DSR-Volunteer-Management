using System.ComponentModel.DataAnnotations;

namespace DatabaseEFC.Utils;

public class Event
{
    [Key]
    public long EventId { get; set; }

    [Required]
    public string EventName { get; set; } = null!;

    [Required]
    public string Date { get; set; } = null!;

    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public string? Location { get; set; }

    public ICollection<Shift>? Shifts { get; set; }
    [Required]
    public ICollection<Manager> Managers { get; set; } = null!;

    //private Event() {}
}