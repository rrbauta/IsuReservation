using IsuReservation.Abstract;
using IsuReservation.Data;
using IsuReservation.Helpers;
using IsuReservation.Models.Entities;
using IsuReservation.Models.Response;
using IsuReservation.Models.ViewModel;

namespace IsuReservation.Impl;

public class ReservationManager : IReservationManager
{
    private readonly AppDbContext _dbContext;

    public ReservationManager(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    ///     List contact and filter by name
    /// </summary>
    /// <returns></returns>
    public async Task<IsuResponse<Paging<ReservationViewModel>>> List(string sortBy, bool sortDesc, int page,
        int recordsPerPage)
    {
        var reservations = _dbContext.Reservations.ToList();

        int totalRecords;
        List<Reservation> myList;

        if (page < 0)
            page = 1;

        var orderList = sortBy switch
        {
            "date" => sortDesc
                ? reservations.OrderByDescending(t => t.Date).ToList()
                : reservations.OrderBy(t => t.Date).ToList(),
            "alphabetic" => sortDesc
                ? reservations.OrderByDescending(t => t.Destination.Name).ToList()
                : reservations.OrderBy(t => t.Destination.Name).ToList(),
            "ranking" => sortDesc
                ? reservations.OrderByDescending(t => t.Destination.Rating).ToList()
                : reservations.OrderBy(t => t.Destination.Rating).ToList(),
            _ => sortDesc
                ? reservations.OrderByDescending(t => t.Date).ToList()
                : reservations.OrderBy(t => t.Date).ToList()
        };

        if (page > 0)
        {
            myList = recordsPerPage < 0
                ? orderList.ToList()
                : orderList.Skip((page - 1) * recordsPerPage).Take(recordsPerPage).ToList();

            totalRecords = orderList.ToList().Count;
        }
        else
        {
            myList = reservations;
            totalRecords = reservations.Count;
        }

        var totalPages = (int) Math.Ceiling((double) totalRecords / recordsPerPage);

        if (page <= 0)
        {
            totalPages = 1;
            page = 0;
            recordsPerPage = totalRecords;
        }

        var response = new Paging<ReservationViewModel>
        {
            RecordsPerPage = recordsPerPage,
            TotalRecords = totalRecords,
            TotalPages = totalPages,
            ActualPage = page,
            Outcome = ReservationHelper.ConvertReservationToViewModel(myList)
        };

        return new IsuResponse<Paging<ReservationViewModel>>(response);
    }
}