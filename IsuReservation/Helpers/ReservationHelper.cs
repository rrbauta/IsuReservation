using IsuReservation.Models.Entities;
using IsuReservation.Models.ViewModel;

namespace IsuReservation.Helpers;

public class ReservationHelper
{
    /// <summary>
    ///     Generate a ReservationViewModel from a Reservation entity
    /// </summary>
    /// <param name="reservation"></param>
    /// <returns></returns>
    public static ReservationViewModel ConvertReservationToViewModel(Reservation reservation)
    {
        var time = DateTime.Today.Add(reservation.Time);
        var displayTime = time.ToString("hh:mm tt"); // It will give "03:00 AM"

        var reservationToViewModel = new ReservationViewModel
        {
            Id = reservation.Id,
            DisplayDate = reservation.Date.ToString("dddd MMMM dd"),
            Date = $"{reservation.Date:MM/dd/yyyy} {reservation.Time.ToString()}",
            Time = displayTime,
            Description = reservation.Description,
            Contact = ContactHelper.ConvertContactToViewModel(reservation.Contact),
            Destination = DestinationHelper.ConvertDestinationToViewModel(reservation.Destination)
        };

        return reservationToViewModel;
    }

    /// <summary>
    ///     Generate a ReservationViewModel list from a Reservation entity list
    /// </summary>
    /// <param name="reservations"></param>
    /// <returns></returns>
    public static List<ReservationViewModel> ConvertReservationToViewModel(List<Reservation> reservations)
    {
        return reservations.Count == 0
            ? new List<ReservationViewModel>()
            : reservations.Select(ConvertReservationToViewModel).ToList();
    }
}