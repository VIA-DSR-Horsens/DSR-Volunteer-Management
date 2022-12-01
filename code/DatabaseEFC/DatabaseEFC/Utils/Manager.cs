using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseEFC.Utils;

public class Manager
{
    [Key]
    public long ManagerId { get; set; }

    [Required]
    public Volunteer Volunteer { get; set; } = null!;

    public ICollection<Event>? EventsManaged { get; set; }

    //private Manager() {}
}