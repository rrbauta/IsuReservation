namespace IsuReservation.Models;

public abstract class BaseDomainEvent
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}