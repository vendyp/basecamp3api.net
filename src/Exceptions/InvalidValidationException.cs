namespace Basecamp3Api.Exceptions;

public class InvalidValidationException : Exception
{
    public InvalidValidationException(string inner) : base(
        "One or more parameter is invalid, please read the documentation", new Exception(inner))
    {
    }
}