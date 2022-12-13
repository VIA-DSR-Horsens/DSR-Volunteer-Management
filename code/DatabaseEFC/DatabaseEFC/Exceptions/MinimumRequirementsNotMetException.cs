namespace DatabaseEFC.Exceptions;

/// <summary>
/// Thrown when minimum requirements haven't been met for proper execution
/// </summary>
public class MinimumRequirementsNotMetException : Exception
{
    public MinimumRequirementsNotMetException()
    {
    }

    public MinimumRequirementsNotMetException(string message)
        : base(message)
    {
    }

    public MinimumRequirementsNotMetException(string message, Exception inner)
        : base(message, inner)
    {
    }
}