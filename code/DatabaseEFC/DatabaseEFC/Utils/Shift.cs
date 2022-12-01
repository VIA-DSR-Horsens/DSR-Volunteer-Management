using System.ComponentModel.DataAnnotations;

namespace DatabaseEFC.Utils;

public class Shift
{
    [Key]
    public long ShiftId { get; set; }

    [Required]
    public Volunteer Volunteer { get; set; }

    [Required]
    public Event Event { get; set; }

    [Required]
    public string StartTime { get; set; }
    
    [Required]
    public string EndTime { get; set; }
    
    //private Shift() {}
}