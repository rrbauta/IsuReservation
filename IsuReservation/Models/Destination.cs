namespace IsuReservation.Models;

public class Destination : BaseEntity
{
    /// <summary>
    ///     Destination name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Destination rating, must be a value between 0 and 5
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    ///     True if dis destination is favorite
    /// </summary>
    public bool Favorite { get; set; }

    /// <summary>
    ///     Description about destination
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Destination Image url
    /// </summary>
    public string Image { get; set; }

    /// <summary>
    ///     Reservation List for this destination
    /// </summary>
    public virtual ICollection<Reservation> Reservations { get; set; }
}