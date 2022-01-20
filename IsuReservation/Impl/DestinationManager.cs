using IsuReservation.Abstract;
using IsuReservation.Data;
using IsuReservation.Helpers;
using IsuReservation.Models.Request;
using IsuReservation.Models.Response;
using IsuReservation.Models.ViewModel;
using IsuReservation.Resources;

namespace IsuReservation.Impl;

public class DestinationManager : IDestinationManager
{
    private readonly AppDbContext _dbContext;

    public DestinationManager(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    ///     List contact and filter by name
    /// </summary>
    /// <returns></returns>
    public async Task<IsuResponse<List<DestinationViewModel>>> List()
    {
        var destinations = _dbContext.Destinations.ToList();

        return new IsuResponse<List<DestinationViewModel>>(
            DestinationHelper.ConvertDestinationToViewModel(destinations));
    }

    /// <summary>
    ///     Destination as favorite
    /// </summary>
    /// <param name="destinationId"></param>
    /// <returns></returns>
    public async Task<IsuResponse<DestinationViewModel>> SetFavorite(Guid destinationId)
    {
        var destination = _dbContext.Destinations.FirstOrDefault(d => d.Id == destinationId);
        if (destination == default)
            return new IsuResponse<DestinationViewModel>(MessageResource.DestinationNotFound);

        destination.Favorite = !destination.Favorite;

        _dbContext.Update(destination);
        await _dbContext.SaveChangesAsync();

        return new IsuResponse<DestinationViewModel>(DestinationHelper.ConvertDestinationToViewModel(destination));
    }

    /// <summary>
    ///     Set destination ranking
    /// </summary>
    /// <param name="destinationId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<IsuResponse<DestinationViewModel>> SetRanking(Guid destinationId, SetRankingRequest request)
    {
        var destination = _dbContext.Destinations.FirstOrDefault(d => d.Id == destinationId);
        if (destination == default)
            return new IsuResponse<DestinationViewModel>(MessageResource.DestinationNotFound);
        if (!int.TryParse(request.Ranking.ToString(), out _))
            return new IsuResponse<DestinationViewModel>(MessageResource.InvalidRankingValue);

        if (request.Ranking is < 0 or > 5)
            return new IsuResponse<DestinationViewModel>(MessageResource.InvalidRankingValue);

        destination.Rating = request.Ranking;

        _dbContext.Update(destination);
        await _dbContext.SaveChangesAsync();

        return new IsuResponse<DestinationViewModel>(DestinationHelper.ConvertDestinationToViewModel(destination));
    }
}