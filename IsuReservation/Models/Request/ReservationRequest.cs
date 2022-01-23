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
    public string Date { get; set; }

    /// <summary>
    ///     Contact name
    /// </summary>
    public string ContactName { get; set; }

    /// <summary>
    ///     Contact phone number
    /// </summary>
    public string ContactPhoneNumber { get; set; }

    /// <summary>
    ///     Contact birthdate
    /// </summary>
    public string ContactBirthDate { get; set; }

    /// <summary>
    ///     Contact type identifier
    /// </summary>
    public Guid ContactTypeId { get; set; }

    /// <summary>
    ///     Contact identifier
    /// </summary>
    public string ContactId { get; set; }

    /// <summary>
    ///     Destination identifier
    /// </summary>
    public Guid DestinationId { get; set; }

    public IsuResponse<ReservationViewModel> Validate()
    {
        if (string.IsNullOrEmpty(Date))
            return new IsuResponse<ReservationViewModel>(MessageResource.DateFieldEmpty);

        if (!DateTime.TryParse(Date.ToString(CultureInfo.CurrentCulture), out _))
            return new IsuResponse<ReservationViewModel>(MessageResource.InvalidDate);

        if (string.IsNullOrEmpty(ContactId))
        {
            if (string.IsNullOrEmpty(ContactName))
                return new IsuResponse<ReservationViewModel>(MessageResource.NameFieldEmpty);

            if (string.IsNullOrEmpty(ContactBirthDate))
                return new IsuResponse<ReservationViewModel>(MessageResource.BirthDayFieldEmpty);

            if (!DateTime.TryParse(ContactBirthDate.ToString(CultureInfo.CurrentCulture), out _))
                return new IsuResponse<ReservationViewModel>(MessageResource.InvalidDate);

            var dateTmp = Convert.ToDateTime(ContactBirthDate);

            if (dateTmp.AddYears(18) > DateTime.Today)
                return new IsuResponse<ReservationViewModel>(MessageResource.AgeOlderThan18);

            if (ContactTypeId == default)
                return new IsuResponse<ReservationViewModel>(MessageResource.ContactTypeFieldEmpty);
        }
        else
        {
            if (!Guid.TryParse(ContactId, out _))
                return new IsuResponse<ReservationViewModel>(MessageResource.ContactFieldEmpty);
        }

        if (DestinationId == default)
            return new IsuResponse<ReservationViewModel>(MessageResource.DestinationFieldEmpty);

        return new IsuResponse<ReservationViewModel>(new ReservationViewModel());
    }
}