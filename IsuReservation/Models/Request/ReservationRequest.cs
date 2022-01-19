using System.Globalization;
using IsuReservation.Models.Response;
using IsuReservation.Models.ViewModel;
using IsuReservation.Resources;

namespace IsuReservation.Models.Request;

public class ReservationRequest
{
    /// <summary>
    ///     Reservation description. Must be string
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
    ///     Contact name
    /// </summary>
    public string ContactName { get; set; }

    /// <summary>
    ///     Contact phone number
    /// </summary>
    public string ContactPhone { get; set; }

    /// <summary>
    ///     Contact birthdate
    /// </summary>
    public DateTime ContactBirthDate { get; set; }

    /// <summary>
    ///     Destination identifier
    /// </summary>
    public Guid DestinationId { get; set; }

    public IsuResponse<ReservationViewModel> Validate()
    {
        if (Date == default)
            return new IsuResponse<ReservationViewModel>(MessageResource.DateFieldEmpty);

        if (!DateTime.TryParse(Date.ToString(CultureInfo.CurrentCulture), out _))
            return new IsuResponse<ReservationViewModel>(MessageResource.InvalidDate);

        if (Time == default)
            return new IsuResponse<ReservationViewModel>(MessageResource.TimeFieldEmpty);

        if (TimeSpan.TryParse(Time.ToString(), out _))
            return new IsuResponse<ReservationViewModel>(MessageResource.InvalidTime);

        if (string.IsNullOrEmpty(ContactName))
            return new IsuResponse<ReservationViewModel>(MessageResource.NameFieldEmpty);

        if (ContactBirthDate == default)
            return new IsuResponse<ReservationViewModel>(MessageResource.BirthDayFieldEmpty);

        if (!DateTime.TryParse(ContactBirthDate.ToString(CultureInfo.CurrentCulture), out _))
            return new IsuResponse<ReservationViewModel>(MessageResource.InvalidDate);

        var dateTmp = Convert.ToDateTime(ContactName);

        if (dateTmp.AddYears(18) > DateTime.Today)
            return new IsuResponse<ReservationViewModel>(MessageResource.AgeOlderThan18);

        if (DestinationId == default)
            return new IsuResponse<ReservationViewModel>(MessageResource.DestinationFieldEmpty);

        return new IsuResponse<ReservationViewModel>(new ReservationViewModel());
    }
}