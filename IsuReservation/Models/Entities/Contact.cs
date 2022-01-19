using System.ComponentModel.DataAnnotations.Schema;

namespace IsuReservation.Models.Entities;

public class Contact : BaseEntity
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
    public DateTime BirthDate { get; set; }

    /// <summary>
    ///     Contact type object. Relation for migration.
    /// </summary>
    [ForeignKey("ContactTypeId")]
    public virtual ContactType ContactType { get; set; }

    /// <summary>
    ///     Contact type identifier
    /// </summary>
    public Guid ContactTypeId { get; set; }

    /// <summary>
    ///     Reservation List for this contact
    /// </summary>
    public virtual ICollection<Reservation> Reservations { get; set; }
}