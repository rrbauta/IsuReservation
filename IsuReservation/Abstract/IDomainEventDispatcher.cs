using IsuReservation.Models;

namespace IsuReservation.Abstract;

public interface IDomainEventDispatcher
{
    void Dispatch(BaseDomainEvent domainEvent);
}