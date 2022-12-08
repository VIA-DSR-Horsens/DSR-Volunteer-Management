namespace VolunteerManager.Models; 

public class User {
    // Authentication
    /// <summary>
    /// The user's email used for authentication
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// The user's password used for authentication
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// The cookie given to the user upon authenticating
    /// </summary>
    public string AuthenticationCookie { get; set; }

    // Properties
    /// <summary>
    /// The user's full name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// How many shifts has the volunteer taken in total
    /// </summary>
    public long ShiftsTaken { get; set; }
    /// <summary>
    /// The overall rating of the volunteer
    /// </summary>
    public long Rating { get; set; }

    // Permissions
    /// <summary>
    /// Can the user create new events
    /// </summary>
    public bool CreateEvents { get; set; }
    /// <summary>
    /// Can the user assign a manager to an event
    /// </summary>
    public bool AssignManagerToEvent { get; set; }
    /// <summary>
    /// Which events, if any, does the user manage. Value is EventId
    /// </summary>
    public ICollection<string> ManagedEvents { get; set; }
    /// <summary>
    /// To which shifts 
    /// </summary>
    public ICollection<string> AssignedShifts { get; set; }
}