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
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<IsuResponse<ContactViewModel>> Create(ContactRequest request);

    /// <summary>
    ///     Update a contact. Return contact updated
    /// </summary>
    /// <param name="request"></param>
    /// <param name="contactId"></param>
    /// <returns></returns>
    public Task<IsuResponse<ContactViewModel>> Update(ContactUpdateRequest request, Guid contactId);

    /// <summary>
    ///     Delete a contact. Return contact deleted
    /// </summary>
    /// <param name="contactId"></param>
    /// <returns></returns>
    public Task<IsuResponse<ContactViewModel>> Delete(Guid contactId);

    /// <summary>
    ///     Contact list. Return all contacts
    /// </summary>
    /// <param name="sortBy"></param>
    /// <param name="sortDesc"></param>
    /// <param name="page"></param>
    /// <param name="recordsPerPage"></param>
    /// <returns></returns>
    public Task<IsuResponse<Paging<ContactViewModel>>>
        List(string? sortBy, bool sortDesc, int page, int recordsPerPage);

    /// <summary>
    ///     Get Contact
    /// </summary>
    /// <param name="contactId"></param>
    /// <returns></returns>
    public Task<IsuResponse<ContactViewModel>> GetContactById(Guid contactId);

    /// <summary>
    ///     Find contact by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<IsuResponse<ContactViewModel>> FindByName(string name);

    /// <summary>
    ///     Find contact by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Contact? FindContactByName(string name);

    /// <summary>
    ///     Find contact by ID
    /// </summary>
    /// <param name="contactId"></param>
    /// <returns></returns>
    public Contact? FindContact(Guid contactId);

    /// <summary>
    ///     Contact Type list. Return all contact types
    /// </summary>
    /// <returns></returns>
    public Task<IsuResponse<List<ContactTypeViewModel>>> ContactTypeList();
}