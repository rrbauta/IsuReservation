using System.Runtime.Serialization;

namespace IsuReservation.Models;

public class IsuException : Exception
{
    public IsuException()
    {
    }

    public IsuException(string message) : base(message)
    {
    }

    public IsuException(string message, Exception inner) : base(message, inner)
    {
    }

    protected IsuException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}