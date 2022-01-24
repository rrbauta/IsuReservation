using IsuReservation.Models.Request;
using IsuReservation.Models.Response;
using IsuReservation.Models.ViewModel;

namespace IsuReservation.Abstract;

public interface IDestinationManager
{
    /// <summary>
    ///     Destination list. Return all destinations. Can be filter by destination name
    /// </summary>
    /// <param name="name">Destination name. Null value is not allowed</param>
    /// <returns>IsuResponse with DestinationViewModel list  or error</returns>
    public Task<IsuResponse<List<DestinationViewModel>>> List(string? name);

    /// <summary>
    /// Update favorite property
    /// </summary>
    /// <param name="destinationId">Destination identifier. Null value is not allowed</param>
    /// <returns>IsuResponse with DestinationViewModel with updated destinations information or error</returns>
    public Task<IsuResponse<DestinationViewModel>> SetFavorite(Guid destinationId);

    /// <summary>
    ///  Update ranking property
    /// </summary>
    /// <param name="destinationId">Destination identifier. Null value is not allowed</param>
    /// <param name="request">SetRankingRequest with data to be updated</param>
    /// <returns>IsuResponse with DestinationViewModel with updated destinations information or error</returns>
    public Task<IsuResponse<DestinationViewModel>> SetRanking(Guid destinationId, SetRankingRequest request);
}