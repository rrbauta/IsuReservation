using IsuReservation.Models.Entities;
using IsuReservation.Models.Request;
using IsuReservation.Models.Response;
using IsuReservation.Models.ViewModel;

namespace IsuReservation.Abstract;

public interface IContactManager
{
    /// <summary>
    ///     Create a contact. Return contact created
    /// </summary>
    /// <param name="request">ContactRequest with contact data to be inserted</param>
    /// <returns>IsuResponse with ContactViewModel with created contact information  or error</returns>
    public Task<IsuResponse<ContactViewModel>> Create(ContactRequest request);

    /// <summary>
    ///     Update a contact. Return contact updated
    /// </summary>
    /// <param name="request">ContactUpdateRequest with contact data to be updated</param>
    /// <param name="contactId">Contact identifier. Null value is not allowed</param>
    /// <returns>IsuResponse with ContactViewModel with created contact information  or error</returns>
    public Task<IsuResponse<ContactViewModel>> Update(ContactUpdateRequest request, Guid contactId);

    /// <summary>
    ///     Delete a contact. Return contact deleted
    /// </summary>
    /// <param name="contactId">Contact identifier. Null value is not allowed </param>
    /// <returns>IsuResponse with ContactViewModel with deleted contact information  or error</returns>
    public Task<IsuResponse<ContactViewModel>> Delete(Guid contactId);

    /// <summary>
    ///     Contact list. Return all contacts
    /// </summary>
    /// <param name="name">Contact name. Null value is allowed</param>
    /// <param name="sortBy">Sort criteria. allowed values are: name, phone, birthDate and contactType. Default value is name</param>
    /// <param name="sortDesc">Sort direction. True means descendent sort</param>
    /// <param name="page">Page number. Default value is 1</param>
    /// <param name="recordsPerPage">Record per page. Default value is 10</param>
    /// <returns>IsuResponse with Paging with ContactViewModel list or error</returns>
    public Task<IsuResponse<Paging<ContactViewModel>>> List(string? name, string? sortBy, bool sortDesc, int page,
        int recordsPerPage);

    /// <summary>
    ///     Get Contact by identifier
    /// </summary>
    /// <param name="contactId">Contact identifier. Null value is not allowed</param>
    /// <returns>IsuResponse with ContactViewModel with retrieved contact information or error</returns>
    public Task<IsuResponse<ContactViewModel>> GetContactById(Guid contactId);

    /// <summary>
    ///     Find contact by name
    /// </summary>
    /// <param name="name">Contact name. Null value is not allowed</param>
    /// <returns>IsuResponse with ContactViewModel with retrieved contact information or error</returns>
    public Task<IsuResponse<ContactViewModel>> FindByName(string name);

    /// <summary>
    ///     Find contact by name
    /// </summary>
    /// <param name="name">Contact name. Null value is not allowed</param>
    /// <returns>IsuResponse with Contact entity with retrieved contact information or error</returns>
    public Contact? FindContactByName(string name);

    /// <summary>
    ///     Find contact by ID
    /// </summary>
    /// <param name="contactId">Contact identifier. Null value is not allowed</param>
    /// <returns>IsuResponse with Contact entity with retrieved contact information</returns>
    public Contact? FindContact(Guid contactId);

    /// <summary>
    ///     Contact Type list. Return all contact types to be used in Reservation and Contact creation forms
    /// </summary>
    /// <returns>IsuResponse with ContactTypeViewModel list</returns>
    public Task<IsuResponse<List<ContactTypeViewModel>>> ContactTypeList();
}