using IsuReservation.Abstract;
using IsuReservation.Data;
using IsuReservation.Helpers;
using IsuReservation.Models.Entities;
using IsuReservation.Models.Request;
using IsuReservation.Models.Response;
using IsuReservation.Models.ViewModel;
using IsuReservation.Resources;

namespace IsuReservation.Impl;

public class ReservationManager : IReservationManager
{
    private readonly AppDbContext _dbContext;
    private readonly IContactManager _contactManager;

    /// <summary>
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="contactManager"></param>
    public ReservationManager(AppDbContext dbContext, IContactManager contactManager)
    {
        _dbContext = dbContext;
        _contactManager = contactManager;
    }

    /// <summary>
    ///     Create a reservation. Return reservation created
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<IsuResponse<ReservationViewModel>> Create(ReservationRequest request)
    {
        var response = request.Validate();
        if (!response.IsSuccess)
            return response;

        var contact = _contactManager.Find(request.ContactName);
        var reservation = _dbContext.Reservations.FirstOrDefault(r =>
            r.Date == request.Date && r.Time == request.Time && r.DestinationId == request.DestinationId &&
            contact != default && r.ContactId == contact.Id);

        if (reservation != default)
            return new IsuResponse<ReservationViewModel>(MessageResource.ReservationAlreadyExist);

        if (contact == default)
            return new IsuResponse<ReservationViewModel>(MessageResource.ContactNotFound);

        var myReservation = new Reservation
        {
            Date = request.Date,
            Time = request.Time,
            Description = request.Description,
            DestinationId = request.DestinationId
        };

        contact.Reservations.Add(myReservation);
        _dbContext.Update(contact);
        await _dbContext.SaveChangesAsync();

        return new IsuResponse<ReservationViewModel>(ReservationHelper.ConvertReservationToViewModel(myReservation));
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