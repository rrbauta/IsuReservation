using System.ComponentModel.DataAnnotations.Schema;

namespace IsuReservation.Models;

public class Reservation : BaseEntity
{
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
    ///     Contact object. Relation for migration.
    /// </summary>
    [ForeignKey("ContactId")]
    public virtual Contact Contact { get; set; }

    /// <summary>
    ///     Contact identifier
    /// </summary>
    public Guid ContactId { get; set; }

    /// <summary>
    ///     Destination object. Relation for migration.
    /// </summary>
    [ForeignKey("DestinationId")]
    public virtual Destination Destination { get; set; }

    /// <summary>
    ///     Destination identifier
    /// </summary>
    public Guid DestinationId { get; set; }
}