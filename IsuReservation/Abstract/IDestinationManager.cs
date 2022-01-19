using IsuReservation.Models.Response;
using IsuReservation.Models.ViewModel;

namespace IsuReservation.Abstract;

public interface IDestinationManager
{
    /// <summary>
    ///     Destination list. Return all destinations
    /// </summary>
    /// <returns></returns>
    public Task<IsuResponse<List<DestinationViewModel>>> List();
}