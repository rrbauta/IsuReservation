using IsuReservation.Models.Request;
using IsuReservation.Models.Response;
using IsuReservation.Models.ViewModel;

namespace IsuReservation.Abstract;

public interface IReservationManager
{
    /// <summary>
    ///     Create a reservation. Return reservation created
    /// </summary>
    /// <param name="request">ReservationRequest with data to be updated</param>
    /// <returns>IsuResponse with ReservationViewModel object with created reservation data or handle error</returns>>
    /// <returns></returns>
    public Task<IsuResponse<ReservationViewModel>> Create(ReservationRequest request);

    /// <summary>
    ///     Update a reservation. Return reservation updated
    /// </summary>
    /// <param name="request">ReservationUpdateRequest with data to be updated</param>
    /// <param name="reservationId">Reservation identifier. Null value is not allowed</param>
    /// <returns>IsuResponse with ReservationViewModel object with updated reservation data or handle error</returns>
    public Task<IsuResponse<ReservationViewModel>> Update(ReservationUpdateRequest request, Guid reservationId);

    /// <summary>
    ///     Reservation list. Return all reservations
    /// </summary>
    /// <param name="sortBy">Sort criteria. allowed values are: date, alphabetic and ranking. Default value is date</param>
    /// <param name="sortDesc">Sort direction. True means descendent sort</param>
    /// <param name="page">Page number. Default value is 1</param>
    /// <param name="recordsPerPage">Record per page. Default value is 10</param>
    /// <returns>IsuResponse with Paging with ReservationViewModel list or error</returns>v
    public Task<IsuResponse<Paging<ReservationViewModel>>> List(string? sortBy, bool sortDesc, int page,
        int recordsPerPage);

    /// <summary>
    ///     Get reservation
    /// </summary>
    /// <param name="reservationId">Reservation identifier. Null value is not allowed</param>
    /// <returns>ReservationViewModel with reservation data or error</returns>
    public Task<IsuResponse<ReservationViewModel>> GetReservationById(Guid reservationId);
}