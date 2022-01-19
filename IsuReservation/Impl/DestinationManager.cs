using IsuReservation.Abstract;
using IsuReservation.Data;
using IsuReservation.Helpers;
using IsuReservation.Models.Response;
using IsuReservation.Models.ViewModel;

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
}