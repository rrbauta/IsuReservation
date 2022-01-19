namespace IsuReservation.Models.Entities;

public class ContactType : BaseEntity
{
    /// <summary>
    ///     Contact type name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Contact relation.
    /// </summary>
    public virtual ICollection<Contact> Contacts { get; set; }
}