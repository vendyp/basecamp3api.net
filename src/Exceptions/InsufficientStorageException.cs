namespace Basecamp3Api.Exceptions;

public class InsufficientStorageException : Exception
{
    public InsufficientStorageException() : base(
        "User will need to upgrade their subscription to any plan, which all have unlimited projects")
    {
    }
}