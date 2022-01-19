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

    // public IsuResponse<ContactViewModel> Validate()
    // {
    //     if (string.IsNullOrEmpty(Name))
    //         return new IsuResponse<ContactViewModel>(MessageResource.NameFieldEmpty);
    //
    //     if (BirthDate == default)
    //         return new IsuResponse<ContactViewModel>(MessageResource.BirthDayFieldEmpty);
    //
    //     if (!DateTime.TryParse(BirthDate.ToString(CultureInfo.CurrentCulture), out _))
    //         return new IsuResponse<ContactViewModel>(MessageResource.InvalidDate);
    //
    //     var dateTmp = Convert.ToDateTime(BirthDate);
    //
    //     if (dateTmp.AddYears(18) > DateTime.Today)
    //         return new IsuResponse<ContactViewModel>(MessageResource.AgeOlderThan18);
    //
    //     if (ContactTypeId == default)
    //         return new IsuResponse<ContactViewModel>(MessageResource.ContactTypeFieldEmpty);
    //
    //     return new IsuResponse<ContactViewModel>(new ContactViewModel());
    // }
}