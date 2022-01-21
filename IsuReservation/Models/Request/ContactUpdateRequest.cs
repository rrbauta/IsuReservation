using System.Globalization;
using System.Text.RegularExpressions;
using IsuReservation.Models.Response;
using IsuReservation.Models.ViewModel;
using IsuReservation.Resources;

namespace IsuReservation.Models.Request;

public class ContactUpdateRequest
{
    /// <summary>
    ///     Contact name. Must be string
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Contact phone number. Must match with fallow regular expression:
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    ///     Contact Birth Date. Must be greater than 18 years old
    /// </summary>
    public string BirthDate { get; set; }

    /// <summary>
    ///     Contact type identifier
    /// </summary>
    public Guid ContactTypeId { get; set; }

    public IsuResponse<ContactViewModel> Validate()
    {
        if (!string.IsNullOrEmpty(BirthDate))
        {
            if (!DateTime.TryParse(BirthDate.ToString(CultureInfo.CurrentCulture), out _))
                return new IsuResponse<ContactViewModel>(MessageResource.InvalidDate);

            var dateTmp = Convert.ToDateTime(BirthDate);

            if (dateTmp.AddYears(18) > DateTime.Today)
                return new IsuResponse<ContactViewModel>(MessageResource.AgeOlderThan18);
        }

        if (string.IsNullOrEmpty(PhoneNumber))
            return new IsuResponse<ContactViewModel>(new ContactViewModel());

        if (!Regex.Match(PhoneNumber, @"^\d{1,3}[\s\.-]?\d{3,4}[\s\.-]?\d{3,4}[\s\.-]?\d{3,4}$").Success)
            return new IsuResponse<ContactViewModel>(MessageResource.InvalidPhoneNumber);

        return new IsuResponse<ContactViewModel>(new ContactViewModel());
    }
}