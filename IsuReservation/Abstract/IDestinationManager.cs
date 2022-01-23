using IsuReservation.Models.Request;
using IsuReservation.Models.Response;
using IsuReservation.Models.ViewModel;

namespace IsuReservation.Abstract;

public interface IDestinationManager
{
    /// <summary>
    ///     Destination list. Return all destinations
    /// </summary>
    /// <returns></returns>
    public Task<IsuResponse<List<DestinationViewModel>>> List(string? name);

    /// <summary>
    ///     Destination as favorite
    /// </summary>
    /// <returns></returns>
    public Task<IsuResponse<DestinationViewModel>> SetFavorite(Guid destinationId);

    /// <summary>
    ///     Set ranking
    /// </summary>
    /// <returns></returns>
    public Task<IsuResponse<DestinationViewModel>> SetRanking(Guid destinationId, SetRankingRequest request);
}