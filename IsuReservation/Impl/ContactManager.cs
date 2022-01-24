using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using IsuReservation.Abstract;
using IsuReservation.Data;
using IsuReservation.Helpers;
using IsuReservation.Models.Entities;
using IsuReservation.Models.Request;
using IsuReservation.Models.Response;
using IsuReservation.Models.ViewModel;
using IsuReservation.Resources;
using Microsoft.EntityFrameworkCore;

namespace IsuReservation.Impl;

public class ContactManager : IContactManager
{
    private readonly AppDbContext _dbContext;

    public ContactManager(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    ///     Create a contact
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<IsuResponse<ContactViewModel>> Create(ContactRequest request)
    {
        var response = request.Validate();
        if (!response.IsSuccess)
            return response;

        Contact? contact = null;
        if (!string.IsNullOrEmpty(request.Name))
        {
            var nameToFind = Regex.Replace(request.Name.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "")
                .ToLower();

            contact = _dbContext.Contacts.ToList().FirstOrDefault(u => Regex
                .Replace(u.Name.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "")
                .ToLower() == nameToFind);
        }

        if (contact != default)
            return new IsuResponse<ContactViewModel>(MessageResource.ContactAlreadyExist);

        var contactType = _dbContext.ContactTypes.FirstOrDefault(ct => ct.Id == request.ContactTypeId);
        if (contactType == default)
            return new IsuResponse<ContactViewModel>(MessageResource.ContactTypeNotFound);

        // var param = new SqlParameter("@Name", name);
        // var contact = _dbContext.Contacts
        //     .FromSqlRaw("dbo.GetContactsFilteredByName @Name", param)
        //     .ToList();

        await _dbContext.Database.ExecuteSqlRawAsync("CreateContact @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7",
            new object[]
            {
                Guid.NewGuid(), request.Name, request.PhoneNumber, request.BirthDate, request.ContactTypeId, false,
                DateTime.Now, DateTime.Now
            });


        // contact = new Contact
        // {
        //     Name = request.Name,
        //     BirthDate = DateTime.ParseExact(request.BirthDate, "MM/dd/yyyy", CultureInfo.CurrentCulture),
        //     ContactTypeId = request.ContactTypeId,
        //     PhoneNumber = request.PhoneNumber,
        //     Reservations = new List<Reservation>()
        // };
        // _dbContext.Add(contact);
        // await _dbContext.SaveChangesAsync();
        //
        // var result = ContactHelper.ConvertContactToViewModel(contact, contactType);
        return new IsuResponse<ContactViewModel>(new ContactViewModel());
    }

    /// <summary>
    ///     Update a contact
    /// </summary>
    /// <param name="request"></param>
    /// <param name="contactId"></param>
    /// <returns></returns>
    public async Task<IsuResponse<ContactViewModel>> Update(ContactUpdateRequest request, Guid contactId)
    {
        var response = request.Validate();
        if (!response.IsSuccess)
            return response;

        var contact = _dbContext.Contacts.FirstOrDefault(c => c.Id == contactId);
        if (contact == default)
            return new IsuResponse<ContactViewModel>(MessageResource.ContactNotFound);

        if (!string.IsNullOrEmpty(request.Name))
            contact.Name = request.Name;

        if (!string.IsNullOrEmpty(request.PhoneNumber))
            contact.PhoneNumber = request.PhoneNumber;

        if (request.ContactTypeId != default)
            contact.ContactTypeId = request.ContactTypeId;

        if (!string.IsNullOrEmpty(request.BirthDate))
            contact.BirthDate = DateTime.ParseExact(request.BirthDate, "MM/dd/yyyy", CultureInfo.CurrentCulture);

        _dbContext.Update(contact);
        await _dbContext.SaveChangesAsync();

        return new IsuResponse<ContactViewModel>(ContactHelper.ConvertContactToViewModel(contact));
    }

    /// <summary>
    ///     Delete a contact
    /// </summary>
    /// <param name="contactId"></param>
    /// <returns></returns>
    public async Task<IsuResponse<ContactViewModel>> Delete(Guid contactId)
    {
        var contact = _dbContext.Contacts.FirstOrDefault(c => c.Id == contactId);
        if (contact == default)
            return new IsuResponse<ContactViewModel>(MessageResource.ContactNotFound);

        var result = ContactHelper.ConvertContactToViewModel(contact);
        contact.Reservations.Clear();
        _dbContext.Remove(contact);
        await _dbContext.SaveChangesAsync();

        return new IsuResponse<ContactViewModel>(result);
    }

    /// <summary>
    ///     List contact
    /// </summary>
    /// <param name="name"></param>
    /// <param name="sortBy"></param>
    /// <param name="sortDesc"></param>
    /// <param name="page"></param>
    /// <param name="recordsPerPage"></param>
    /// <returns></returns>
    public async Task<IsuResponse<Paging<ContactViewModel>>> List(string? name, string? sortBy, bool sortDesc, int page,
        int recordsPerPage)
    {
        var contacts = _dbContext.Contacts.ToList();
        int totalRecords;
        List<Contact> myList;

        if (page < 0)
            page = 1;

        if (!string.IsNullOrEmpty(name))
        {
            var nameToFind = Regex.Replace(name.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "")
                .ToLower();

            contacts = _dbContext.Contacts.ToList().Where(u => Regex
                .Replace(u.Name.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "")
                .ToLower().Contains(nameToFind)).ToList();
        }

        var orderList = sortBy switch
        {
            "name" => sortDesc
                ? contacts.OrderByDescending(t => t.Name).ToList()
                : contacts.OrderBy(t => t.Name).ToList(),
            "phone" => sortDesc
                ? contacts.OrderByDescending(t => t.PhoneNumber).ToList()
                : contacts.OrderBy(t => t.PhoneNumber).ToList(),
            "birthDate" => sortDesc
                ? contacts.OrderByDescending(t => t.BirthDate).ToList()
                : contacts.OrderBy(t => t.BirthDate).ToList(),
            "contactType" => sortDesc
                ? contacts.OrderByDescending(t => t.ContactType.Name).ToList()
                : contacts.OrderBy(t => t.ContactType.Name).ToList(),
            _ => sortDesc
                ? contacts.OrderByDescending(t => t.Name).ToList()
                : contacts.OrderBy(t => t.Name).ToList()
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
            myList = contacts;
            totalRecords = contacts.Count;
        }

        var totalPages = (int) Math.Ceiling((double) totalRecords / recordsPerPage);

        if (page <= 0)
        {
            totalPages = 1;
            page = 0;
            recordsPerPage = totalRecords;
        }

        var response = new Paging<ContactViewModel>
        {
            RecordsPerPage = recordsPerPage,
            TotalRecords = totalRecords,
            TotalPages = totalPages,
            ActualPage = page,
            Outcome = ContactHelper.ConvertContactToViewModel(myList)
        };

        return new IsuResponse<Paging<ContactViewModel>>(response);
    }

    public async Task<IsuResponse<ContactViewModel>> GetContactById(Guid contactId)
    {
        if (contactId == default)
            return new IsuResponse<ContactViewModel>(MessageResource.ContactNotFound);

        var contact = _dbContext.Contacts.FirstOrDefault(u => u.Id == contactId);
        if (contact == default)
            return new IsuResponse<ContactViewModel>(MessageResource.ContactNotFound);

        return new IsuResponse<ContactViewModel>(ContactHelper.ConvertContactToViewModel(contact));
    }

    /// <summary>
    ///     Find contact by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<IsuResponse<ContactViewModel>> FindByName(string name)
    {
        var nameToFind = Regex.Replace(name.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "")
            .ToLower();

        var contact = _dbContext.Contacts.ToList().FirstOrDefault(u => Regex
            .Replace(u.Name.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "")
            .ToLower().Contains(nameToFind));

        return contact != default
            ? new IsuResponse<ContactViewModel>(ContactHelper.ConvertContactToViewModel(contact))
            : new IsuResponse<ContactViewModel>(new ContactViewModel());
    }

    /// <summary>
    ///     Find contact by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Contact? FindContactByName(string name)
    {
        var nameToFind = Regex.Replace(name.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "")
            .ToLower();

        var contact = _dbContext.Contacts.ToList().FirstOrDefault(u => Regex
            .Replace(u.Name.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "")
            .ToLower() == nameToFind);

        return contact;
    }

    /// <summary>
    ///     Find contact by ID
    /// </summary>
    /// <param name="contactId"></param>
    /// <returns></returns>
    public Contact? FindContact(Guid contactId)
    {
        var contact = _dbContext.Contacts.FirstOrDefault(u => u.Id == contactId);

        return contact;
    }

    public async Task<IsuResponse<List<ContactTypeViewModel>>> ContactTypeList()
    {
        var contactTypes = _dbContext.ContactTypes.ToList();

        return new IsuResponse<List<ContactTypeViewModel>>(ContactHelper.ConvertContactTypeToViewModel(contactTypes));
    }
}