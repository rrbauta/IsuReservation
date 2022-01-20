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

        var contact = request.ContactId != default
            ? _contactManager.FindContact(request.ContactId)
            : _contactManager.FindContactByName(request.ContactName);

        if (contact == default)
        {
            var contactCreated = await _contactManager.Create(new ContactRequest
            {
                Name = request.ContactName,
                BirthDate = request.ContactBirthDate,
                PhoneNumber = request.ContactPhone,
                ContactTypeId = request.ContactTypeId
            });

            if (!contactCreated.IsSuccess)
                return new IsuResponse<ReservationViewModel>(contactCreated.Exception);

            contact = _contactManager.FindContact(contactCreated.Data.Id);
            if (contact == default)
                return new IsuResponse<ReservationViewModel>(MessageResource.ContactNotFound);
        }

        var reservation = _dbContext.Reservations.FirstOrDefault(r =>
            r.Date == request.Date && r.Time == request.Time && r.DestinationId == request.DestinationId &&
            r.ContactId == contact.Id);

        if (reservation != default)
            return new IsuResponse<ReservationViewModel>(MessageResource.ReservationAlreadyExist);

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
    ///     Update a reservation. Return reservation updated
    /// </summary>
    /// <param name="request"></param>
    /// <param name="reservationId"></param>
    /// <returns></returns>
    public async Task<IsuResponse<ReservationViewModel>> Update(ReservationUpdateRequest request, Guid reservationId)
    {
        var response = request.Validate();
        if (!response.IsSuccess)
            return response;

        var reservation = _dbContext.Reservations.FirstOrDefault(r => r.Id == reservationId);
        if (reservation == default)
            return new IsuResponse<ReservationViewModel>(MessageResource.ReservationNotFound);

        var contact = request.ContactId != default
            ? _contactManager.FindContact(request.ContactId)
            : _contactManager.FindContactByName(request.ContactName);

        if (contact == default)
        {
            var contactCreated = await _contactManager.Create(new ContactRequest
            {
                Name = request.ContactName,
                BirthDate = request.ContactBirthDate,
                PhoneNumber = request.ContactPhone,
                ContactTypeId = request.ContactTypeId
            });

            if (!contactCreated.IsSuccess)
                return new IsuResponse<ReservationViewModel>(contactCreated.Exception);

            contact = _contactManager.FindContact(contactCreated.Data.Id);

            if (contact == default)
                return new IsuResponse<ReservationViewModel>(MessageResource.ContactNotFound);
        }

        if (request.Date != default)
            reservation.Date = request.Date;

        if (request.Time != default)
            reservation.Time = request.Time;

        if (!string.IsNullOrEmpty(request.Description))
            reservation.Description = request.Description;

        if (request.DestinationId != default)
            reservation.DestinationId = request.DestinationId;

        reservation.ContactId = request.ContactId != default ? request.ContactId : contact.Id;

        _dbContext.Update(reservation);
        await _dbContext.SaveChangesAsync();

        return new IsuResponse<ReservationViewModel>(ReservationHelper.ConvertReservationToViewModel(reservation));
    }

    /// <summary>
    ///     List contact and filter by name
    /// </summary>
    /// <returns></returns>
    public async Task<IsuResponse<Paging<ReservationViewModel>>> List(string? sortBy, bool sortDesc, int page,
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