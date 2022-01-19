namespace IsuReservation.Models.ViewModel;

public class DestinationViewModel
{
    /// <summary>
    ///     Destination identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Destination name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Destination rating
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
}