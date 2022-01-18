using System.Runtime.Serialization;

namespace IsuReservation.Models;

public class MyException : Exception
{
    public MyException()
    {
    }

    public MyException(string message) : base(message)
    {
    }

    public MyException(string message, Exception inner) : base(message, inner)
    {
    }

    protected MyException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}