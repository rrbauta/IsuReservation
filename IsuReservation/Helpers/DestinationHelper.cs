using IsuReservation.Models.Entities;
using IsuReservation.Models.ViewModel;

namespace IsuReservation.Helpers;

public class DestinationHelper
{
    /// <summary>
    ///     Generate a DestinationViewModel from a Destination entity
    /// </summary>
    /// <param name="destination"></param>
    /// <returns></returns>
    public static DestinationViewModel ConvertDestinationToViewModel(Destination destination)
    {
        var destinationViewModel = new DestinationViewModel
        {
            Id = destination.Id,
            Description = destination.Description,
            Favorite = destination.Favorite,
            Image = destination.Image,
            Name = destination.Name,
            Rating = destination.Rating
        };

        return destinationViewModel;
    }

    /// <summary>
    ///     Generate a DestinationViewModel list from a Destination entity list
    /// </summary>
    /// <param name="destinations"></param>
    /// <returns></returns>
    public static List<DestinationViewModel> ConvertDestinationToViewModel(List<Destination> destinations)
    {
        return destinations.Count == 0
            ? new List<DestinationViewModel>()
            : destinations.Select(ConvertDestinationToViewModel).ToList();
    }
}