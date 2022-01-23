using System.Globalization;
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

        var contact = !string.IsNullOrEmpty(request.ContactId)
            ? _contactManager.FindContact(Guid.Parse(request.ContactId))
            : _contactManager.FindContactByName(request.ContactName);

        if (contact == default)
        {
            var contactCreated = await _contactManager.Create(new ContactRequest
            {
                Name = request.ContactName,
                BirthDate = request.ContactBirthDate,
                PhoneNumber = request.ContactPhoneNumber,
                ContactTypeId = request.ContactTypeId
            });

            if (!contactCreated.IsSuccess)
                return new IsuResponse<ReservationViewModel>(contactCreated.Exception);

            contact = _contactManager.FindContact(contactCreated.Data.Id);
            if (contact == default)
                return new IsuResponse<ReservationViewModel>(MessageResource.ContactNotFound);
        }
        else
        {
            await _contactManager.Update(new ContactUpdateRequest
            {
                Name = request.ContactName,
                BirthDate = request.ContactBirthDate,
                PhoneNumber = request.ContactPhoneNumber,
                ContactTypeId = request.ContactTypeId
            }, contact.Id);
        }

        var date = DateTime.Parse(request.Date);
        var time = TimeSpan.Parse(date.ToString("hh:mm:ss"));

        var reservation = _dbContext.Reservations.FirstOrDefault(r =>
            r.Date == date && r.Time == time && r.DestinationId == request.DestinationId &&
            r.ContactId == contact.Id);

        if (reservation != default)
            return new IsuResponse<ReservationViewModel>(MessageResource.ReservationAlreadyExist);

        var myReservation = new Reservation
        {
            Date = DateTime.ParseExact(date.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.CurrentCulture),
            Time = time,
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
                PhoneNumber = request.ContactPhoneNumber,
                ContactTypeId = request.ContactTypeId
            });

            if (!contactCreated.IsSuccess)
                return new IsuResponse<ReservationViewModel>(contactCreated.Exception);

            contact = _contactManager.FindContact(contactCreated.Data.Id);

            if (contact == default)
                return new IsuResponse<ReservationViewModel>(MessageResource.ContactNotFound);
        }
        else
        {
            await _contactManager.Update(new ContactUpdateRequest
            {
                Name = request.ContactName,
                BirthDate = request.ContactBirthDate,
                PhoneNumber = request.ContactPhoneNumber,
                ContactTypeId = request.ContactTypeId
            }, contact.Id);
        }

        if (!string.IsNullOrEmpty(request.Date))
        {
            var date = DateTime.Parse(request.Date);
            var time = TimeSpan.Parse(date.ToString("hh:mm:ss"));

            reservation.Date =
                DateTime.ParseExact(date.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.CurrentCulture);
            reservation.Time = time;
        }

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

        if (page > 0)
        {
            myList = recordsPerPage < 0
                ? reservations.ToList()
                : reservations.Skip((page - 1) * recordsPerPage).Take(recordsPerPage).ToList();

            totalRecords = reservations.ToList().Count;
        }
        else
        {
            myList = reservations;
            totalRecords = reservations.Count;
        }

        var orderList = sortBy switch
        {
            "date" => sortDesc
                ? myList.OrderByDescending(t => t.Date).ToList()
                : myList.OrderBy(t => t.Date).ToList(),
            "alphabetic" => sortDesc
                ? myList.OrderByDescending(t => t.Destination.Name).ToList()
                : myList.OrderBy(t => t.Destination.Name).ToList(),
            "ranking" => sortDesc
                ? myList.OrderByDescending(t => t.Destination.Rating).ToList()
                : myList.OrderBy(t => t.Destination.Rating).ToList(),
            _ => sortDesc
                ? myList.OrderByDescending(t => t.Date).ToList()
                : myList.OrderBy(t => t.Date).ToList()
        };

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
            Outcome = ReservationHelper.ConvertReservationToViewModel(orderList)
        };

        return new IsuResponse<Paging<ReservationViewModel>>(response);
    }

    /// <summary>
    ///     Get contact by identifier
    /// </summary>
    /// <param name="reservationId"></param>
    /// <returns></returns>
    public async Task<IsuResponse<ReservationViewModel>> GetReservationById(Guid reservationId)
    {
        if (reservationId == default)
            return new IsuResponse<ReservationViewModel>(MessageResource.ReservationNotFound);

        var reservation = _dbContext.Reservations.FirstOrDefault(u => u.Id == reservationId);
        if (reservation == default)
            return new IsuResponse<ReservationViewModel>(MessageResource.ContactNotFound);

        return new IsuResponse<ReservationViewModel>(ReservationHelper.ConvertReservationToViewModel(reservation));
    }
}