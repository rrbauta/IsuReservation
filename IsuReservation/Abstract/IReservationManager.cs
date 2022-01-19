using IsuReservation.Models.Request;
using IsuReservation.Models.Response;
using IsuReservation.Models.ViewModel;

namespace IsuReservation.Abstract;

public interface IReservationManager
{
    /// <summary>
    ///     Create a reservation. Return reservation created
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<IsuResponse<ReservationViewModel>> Create(ReservationRequest request);

    /// <summary>
    ///     Update a reservation. Return reservation updated
    /// </summary>
    /// <param name="request"></param>
    /// <param name="reservationId"></param>
    /// <returns></returns>
    public Task<IsuResponse<ReservationViewModel>> Update(ReservationUpdateRequest request, Guid reservationId);

    /// <summary>
    ///     Reservation list. Return all reservations
    /// </summary>
    /// <param name="sortBy"></param>
    /// <param name="sortDesc"></param>
    /// <param name="page"></param>
    /// <param name="recordsPerPage"></param>
    /// <returns></returns>
    public Task<IsuResponse<Paging<ReservationViewModel>>> List(string sortBy, bool sortDesc, int page,
        int recordsPerPage);
}