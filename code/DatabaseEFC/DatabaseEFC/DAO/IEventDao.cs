using DatabaseEFC.Utils;

namespace DatabaseEFC.DAO;

/// <summary>
/// Used to execute queries related to events
/// </summary>
public interface IEventDao
{
    /// <summary>
    /// Gets the event based off id
    /// </summary>
    /// <param name="eventId">The event's id</param>
    /// <returns>The event</returns>
    Task<Event> GetByIdAsync(long eventId);

    /// <summary>
    /// Create an event in the database
    /// </summary>
    /// <param name="eventDTO">The event to create</param>
    /// <returns>The newly created event</returns>
    Task<Event> CreateAsync(DTO.Event eventDTO);

    /// <summary>
    /// Gets all the events in the database
    /// </summary>
    /// <returns>A list of all events</returns>
    Task<List<Event>> GetAsync();
}