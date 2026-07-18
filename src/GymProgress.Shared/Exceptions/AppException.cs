namespace GymProgress.Shared.Exceptions;

/// <summary>
/// Base type for all predictable, business-meaningful exceptions in the system.
/// Every derived exception carries the HTTP status code that should be returned
/// to the caller, so the API layer never has to guess how to translate it.
/// </summary>
public abstract class AppException : Exception
{
    public int StatusCode { get; }

    protected AppException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}
