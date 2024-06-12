namespace Framework.Core.Exceptions;

public abstract class BaseApplicationException : Exception
{
    protected BaseApplicationException()
    {
    }

    protected BaseApplicationException(string message) : base(message)
    {
    }

    public virtual int StatusCode => 500;
}