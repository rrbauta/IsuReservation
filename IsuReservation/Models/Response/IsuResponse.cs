namespace IsuReservation.Models.Response;

public class IsuResponse<T>
{
    public T Data { get; }

    public bool IsSuccess => Exception == null;

    public Exception Exception { get; }

    public IsuResponse(Exception exception)
    {
        Exception = exception;
    }

    public IsuResponse(string message)
    {
        Exception = new IsuException(message);
    }

    public IsuResponse(string message, Exception innerException)
    {
        Exception = new IsuException(message, innerException);
    }

    public IsuResponse(T data)
    {
        Data = data;
    }
}