using IsuReservation.Models.Entities;
using IsuReservation.Models.ViewModel;

namespace IsuReservation.Helpers;

public class ContactHelper
{
    /// <summary>
    ///     Generate a ContactViewModel from a Contact entity
    /// </summary>
    /// <param name="contact"></param>
    /// <param name="contactType"></param>
    /// <returns></returns>
    public static ContactViewModel ConvertContactToViewModel(Contact contact, ContactType? contactType = default)
    {
        var contactViewModel = new ContactViewModel
        {
            Id = contact.Id,
            Name = contact.Name,
            BirthDate = contact.BirthDate.ToString("MM/dd/yyyy"),
            PhoneNumber = contact.PhoneNumber,
            ContactTypeId = contactType?.Id ?? contact.ContactType.Id,
            ContactTypeName = contactType?.Name ?? contact.ContactType.Name
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
            : contacts.Select(c => ConvertContactToViewModel(c)).ToList();
    }

    /// <summary>
    ///     Generate a ContactTypeViewModel from a ContactType entity
    /// </summary>
    /// <param name="contactType"></param>
    /// <returns></returns>
    public static ContactTypeViewModel ConvertContactTypeToViewModel(ContactType contactType)
    {
        var contactViewModel = new ContactTypeViewModel
        {
            Id = contactType.Id,
            Name = contactType.Name
        };

        return contactViewModel;
    }

    /// <summary>
    ///     Generate a ContactTypeViewModel list from a ContactType entity list
    /// </summary>
    /// <param name="contactTypes"></param>
    /// <returns></returns>
    public static List<ContactTypeViewModel> ConvertContactTypeToViewModel(List<ContactType> contactTypes)
    {
        return contactTypes.Count == 0
            ? new List<ContactTypeViewModel>()
            : contactTypes.Select(ConvertContactTypeToViewModel).ToList();
    }
}