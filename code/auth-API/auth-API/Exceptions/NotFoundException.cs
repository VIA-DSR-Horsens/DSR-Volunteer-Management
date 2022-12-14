namespace auth_API.Exceptions;

/// <summary>
/// Thrown whenever there's an exception establishing a connection to an external server
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}