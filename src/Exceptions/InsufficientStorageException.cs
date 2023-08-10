namespace Basecamp3Api.Exceptions;

public class InsufficientStorageException : Exception
{
    public InsufficientStorageException() : base(
        "The project limit for this account has been reached")
    {
    }
}