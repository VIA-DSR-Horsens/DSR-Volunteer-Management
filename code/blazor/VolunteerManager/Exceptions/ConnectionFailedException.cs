namespace VolunteerManager.Exceptions;

/// <summary>
/// Thrown whenever there's an exception establishing a connection to an external server
/// </summary>
public class ConnectionFailedException : Exception
{
    public ConnectionFailedException()
    {
    }

    public ConnectionFailedException(string message)
        : base(message)
    {
    }

    public ConnectionFailedException(string message, Exception inner)
        : base(message, inner)
    {
    }
}