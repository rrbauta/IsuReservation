using IsuReservation.Models.Response;
using IsuReservation.Models.ViewModel;
using IsuReservation.Resources;

namespace IsuReservation.Models.Request;

public class ReservationUpdateRequest
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
    public Guid ContactId { get; set; }

    /// <summary>
    ///     Destination identifier
    /// </summary>
    public Guid DestinationId { get; set; }

    public IsuResponse<ReservationViewModel> Validate()
    {
        if (!string.IsNullOrEmpty(Date))
            if (!DateTime.TryParse(Date, out _))
                return new IsuResponse<ReservationViewModel>(MessageResource.InvalidDate);

        if (ContactId != default)
        {
            if (string.IsNullOrEmpty(ContactBirthDate))
                return new IsuResponse<ReservationViewModel>(new ReservationViewModel());

            if (!DateTime.TryParse(ContactBirthDate, out _))
                return new IsuResponse<ReservationViewModel>(MessageResource.InvalidDate);

            var dateTmp = Convert.ToDateTime(ContactBirthDate);

            if (dateTmp.AddYears(18) > DateTime.Today)
                return new IsuResponse<ReservationViewModel>(MessageResource.AgeOlderThan18);
        }


        return new IsuResponse<ReservationViewModel>(new ReservationViewModel());
    }
}