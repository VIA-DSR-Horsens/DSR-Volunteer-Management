using System.ComponentModel.DataAnnotations;

namespace DatabaseEFC.Utils;

public class Administrator
{
    [Key]
    public long AdministratorId { get; set; }

    [Required]
    public Manager Manager { get; set; }
    
    //private Administrator() {}
}