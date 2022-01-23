using IsuReservation.Abstract;
using IsuReservation.Models.Request;
using IsuReservation.Models.Response;
using IsuReservation.Models.ViewModel;
using IsuReservation.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IsuReservation.Controllers;

/// <summary>
/// </summary>
[Route("api/contacts")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IContactManager _contactManager;
    private readonly ILogger<ContactController> _logger;

    /// <summary>
    /// </summary>
    /// <param name="contactManager"></param>
    /// <param name="logger"></param>
    public ContactController(IContactManager contactManager, ILogger<ContactController> logger)
    {
        _contactManager = contactManager;
        _logger = logger;
    }

    /// <summary>
    ///     Create a contact
    /// </summary>
    /// <remarks>
    ///     <para>Sample request:</para>
    ///     POST api/contact
    ///     {
    ///     "name": "John Doe",
    ///     "phoneNumber": "7866772441",
    ///     "birthDate": "20/06/1985",
    ///     "contactTypeId": "437583d1-077f-428e-89ea-302d90c81352",
    ///     }
    /// </remarks>
    /// <param name="requestModel"></param>
    /// <returns>Contact created</returns>
    [HttpPost]
    [ProducesResponseType(typeof(IsuErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] ContactRequest requestModel)
    {
        _logger.LogInformation("Create contact using data: {Request}", JsonConvert.SerializeObject(requestModel));

        try
        {
            var response = await _contactManager.Create(requestModel);

            if (response.IsSuccess)
            {
                _logger.LogInformation("Create contact finish: {Response}", JsonConvert.SerializeObject(response));

                return Ok(response);
            }

            _logger.LogError("Create contact failed: {Response}", JsonConvert.SerializeObject(response));

            return BadRequest(new IsuErrorResponse(response.Exception.Message));
        }
        catch (Exception e)
        {
            _logger.LogError("Create contact failed. Caught Exception: {Error}", JsonConvert.SerializeObject(e));

            return BadRequest(new IsuErrorResponse(MessageResource.UnhandledError));
        }
    }

    /// <summary>
    ///     Update a contact
    /// </summary>
    /// <remarks>
    ///     <para>Sample request:</para>
    ///     PUT api/contact/ca9f3e23-6197-4fdd-8c90-ec9779af2a91
    ///     {
    ///     "name": "John Doe",
    ///     "phoneNumber": "7866772441",
    ///     "birthDate": "20/06/1985",
    ///     "contactTypeId": "437583d1-077f-428e-89ea-302d90c81352",
    ///     }
    /// </remarks>
    /// <param name="requestModel"></param>
    /// <param name="contactId"></param>
    /// <returns>Contact updated</returns>
    [HttpPut]
    [Route("{contactId:guid}")]
    [ProducesResponseType(typeof(IsuErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromBody] ContactUpdateRequest requestModel, [FromRoute] Guid contactId)
    {
        _logger.LogInformation("Update contact using data: {Request}", JsonConvert.SerializeObject(requestModel));

        try
        {
            var response = await _contactManager.Update(requestModel, contactId);

            if (response.IsSuccess)
            {
                _logger.LogInformation("Update contact finish: {Response}", JsonConvert.SerializeObject(response));

                return Ok(response);
            }

            _logger.LogError("Create contact failed: {Response}", JsonConvert.SerializeObject(response));

            return BadRequest(new IsuErrorResponse(response.Exception.Message));
        }
        catch (Exception e)
        {
            _logger.LogError("Update contact failed. Caught Exception: {Error}", JsonConvert.SerializeObject(e));

            return BadRequest(new IsuErrorResponse(MessageResource.UnhandledError));
        }
    }

    /// <summary>
    ///     Delete a contact
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="contactId"></param>
    /// <returns></returns>
    [Route("{contactId:guid}")]
    [HttpDelete]
    [ProducesResponseType(typeof(IsuErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromRoute] Guid contactId)
    {
        _logger.LogInformation("Delete a contact using data: offerId {ContactId}", contactId);
        try
        {
            var response = await _contactManager.Delete(contactId);

            if (response.IsSuccess)
            {
                _logger.LogInformation("Delete a contact finish: {Response}", JsonConvert.SerializeObject(response));

                return Ok(response);
            }

            _logger.LogError("Delete an offer failed: {Response}", JsonConvert.SerializeObject(response));

            return BadRequest(new IsuErrorResponse(response.Exception.Message));
        }
        catch (Exception e)
        {
            _logger.LogError("Delete an offer failed. Caught Exception: {Error}", JsonConvert.SerializeObject(e));

            return BadRequest(new IsuErrorResponse(MessageResource.UnhandledError));
        }
    }

    /// <summary>
    ///     Contact list. This list is paginated
    /// </summary>
    /// <returns>Return a contact list.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IsuErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Paging<ContactViewModel>>> List([FromQuery] string? name,
        [FromQuery] string? sortBy = "name",
        [FromQuery] bool sortDesc = false, [FromQuery] int page = 1, [FromQuery] int recordsPerPage = 10)
    {
        _logger.LogInformation("Get contact list to show in table");

        try
        {
            var response = await _contactManager.List(name, sortBy, sortDesc, page, recordsPerPage);

            if (response.IsSuccess)
            {
                _logger.LogInformation(
                    "Get contact list to show in table finish: {Response}", JsonConvert.SerializeObject(response));

                return Ok(response);
            }

            _logger.LogError("Get contact list to show in table failed: {Response}",
                JsonConvert.SerializeObject(response));

            return BadRequest(new IsuErrorResponse(response.Exception.Message));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "Get contact list to show in table failed. Caught exception: {Error}", JsonConvert.SerializeObject(e));

            return BadRequest(new IsuErrorResponse(MessageResource.UnhandledError));
        }
    }

    /// <summary>
    ///     Get Contact by identifier
    /// </summary>
    /// <returns>Return a contact.</returns>
    [HttpGet]
    [Route("{contactId:guid}")]
    [ProducesResponseType(typeof(IsuErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Paging<ContactViewModel>>> GetContactById([FromRoute] Guid contactId)
    {
        _logger.LogInformation("Get contact to show in table");

        try
        {
            var response = await _contactManager.GetContactById(contactId);

            if (response.IsSuccess)
            {
                _logger.LogInformation(
                    "Get contact finish: {Response}", JsonConvert.SerializeObject(response));

                return Ok(response);
            }

            _logger.LogError("Get contact failed: {Response}",
                JsonConvert.SerializeObject(response));

            return BadRequest(new IsuErrorResponse(response.Exception.Message));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "Get contact. Caught exception: {Error}", JsonConvert.SerializeObject(e));

            return BadRequest(new IsuErrorResponse(MessageResource.UnhandledError));
        }
    }

    /// <summary>
    ///     Contact by name to complete reservation form
    /// </summary>
    /// <returns>Return a contact.</returns>
    [HttpGet]
    [Route("by-name")]
    [ProducesResponseType(typeof(IsuErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Paging<ContactViewModel>>> FindByName([FromQuery] string name)
    {
        _logger.LogInformation("Get contact by name to complete reservation form");

        try
        {
            var response = await _contactManager.FindByName(name);

            if (response.IsSuccess)
            {
                _logger.LogInformation(
                    "Get contact by name to complete reservation form finish: {Response}",
                    JsonConvert.SerializeObject(response));

                return Ok(response);
            }

            _logger.LogError(
                "Get contact by name to complete reservation form failed: {Response}",
                JsonConvert.SerializeObject(response));

            return BadRequest(new IsuErrorResponse(response.Exception.Message));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "Get contact by name to complete reservation form failed. Caught exception: {Error}",
                JsonConvert.SerializeObject(e));

            return BadRequest(new IsuErrorResponse(MessageResource.UnhandledError));
        }
    }


    /// <summary>
    ///     Contact type list to used in reservation and contact form
    /// </summary>
    /// <returns>Return a contact type list.</returns>
    [HttpGet]
    [Route("types")]
    [ProducesResponseType(typeof(IsuErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Paging<ContactViewModel>>> ContactTypeList()
    {
        _logger.LogInformation("Get contact type list to show in table");

        try
        {
            var response = await _contactManager.ContactTypeList();

            if (response.IsSuccess)
            {
                _logger.LogInformation(
                    "Get contact type  list to show in table finish: {Response}",
                    JsonConvert.SerializeObject(response));

                return Ok(response);
            }

            _logger.LogError("Get contact type  list to show in table failed: {Response}",
                JsonConvert.SerializeObject(response));

            return BadRequest(new IsuErrorResponse(response.Exception.Message));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "Get contact type  list to show in table failed. Caught exception: {Error}",
                JsonConvert.SerializeObject(e));

            return BadRequest(new IsuErrorResponse(MessageResource.UnhandledError));
        }
    }
}