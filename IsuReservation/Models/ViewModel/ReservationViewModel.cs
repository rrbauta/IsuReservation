namespace IsuReservation.Models.ViewModel;

public class ReservationViewModel
{
    /// <summary>
    ///     Reservation identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Reservation description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Reservation date
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    ///     Reservation time
    /// </summary>
    public TimeSpan Time { get; set; }

    /// <summary>
    ///     Contact
    /// </summary>
    public ContactViewModel Contact { get; set; }

    /// <summary>
    ///     Destination
    /// </summary>
    public DestinationViewModel Destination { get; set; }
}