using IsuReservation.Models.Entities;
using IsuReservation.Models.ViewModel;

namespace IsuReservation.Helpers;

public class ContactHelper
{
    /// <summary>
    ///     Generate a ContactViewModel from a Contact entity
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    public static ContactViewModel ConvertContactToViewModel(Contact contact)
    {
        var contactViewModel = new ContactViewModel
        {
            Id = contact.Id,
            Name = contact.Name,
            BirthDate = contact.BirthDate,
            ContactTypeId = contact.ContactTypeId,
            PhoneNumber = contact.PhoneNumber,
            ContactTypeName = contact.ContactType.Name
        };

        return contactViewModel;
    }

    /// <summary>
    ///     Generate a ContactViewModel list from a Contact entity list
    /// </summary>
    /// <param name="contacts"></param>
    /// <returns></returns>
    public static List<ContactViewModel> ConvertContactToViewModel(List<Contact> contacts)
    {
        return contacts.Count == 0
            ? new List<ContactViewModel>()
            : contacts.Select(ConvertContactToViewModel).ToList();
    }
}