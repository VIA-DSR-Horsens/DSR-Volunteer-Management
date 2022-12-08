namespace VolunteerManager.Models; 

/// <summary>
/// The user's information besides authentication data
/// </summary>
public class User {
    // Authorization
    /// <summary>
    /// The authorization cookie used to authorize
    /// </summary>
    public string AuthCookie { get; set; } = null!;

    // Properties
    /// <summary>
    /// The user's email
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// The user's full name
    /// </summary>
    public string FullName { get; set; } = null!;

    /// <summary>
    /// How many shifts has the volunteer taken in total
    /// </summary>
    public long ShiftsTaken { get; set; }
    /// <summary>
    /// The overall rating of the volunteer
    /// </summary>
    public long Rating { get; set; }
    /// <summary>
    /// The shifts the user is currently assigned to. Value is shift id
    /// </summary>
    public ICollection<string> Shifts { get; set; } = null!;

    /// <summary>
    /// The shifts which the user wants to have. Value is shift id
    /// </summary>
    public ICollection<string> RequestedShifts { get; set; } = null!;

    /// <summary>
    /// Events which the user is currently managing. Value is event id
    /// </summary>
    public ICollection<string> EventsManaged { get; set; } = null!;

    // Role related stuff
    /// <summary>
    /// Whether the user is a manager
    /// </summary>
    public bool IsManager { get; set; }
    /// <summary>
    /// Whether the user is an administrator
    /// </summary>
    public bool IsAdministrator { get; set; }
}