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
[Route("api/reservations")]
[ApiController]
public class ReservationController : ControllerBase
{
    private readonly IReservationManager _reservationManager;
    private readonly ILogger<ReservationController> _logger;

    /// <summary>
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="reservationManager"></param>
    public ReservationController(ILogger<ReservationController> logger, IReservationManager reservationManager)
    {
        _logger = logger;
        _reservationManager = reservationManager;
    }

    /// <summary>
    ///     Create a reservation
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
    /// <returns>Reservation created</returns>
    [HttpPost]
    [ProducesResponseType(typeof(IsuErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromForm] ReservationRequest requestModel)
    {
        _logger.LogInformation("Create reservation using data: {Request}", JsonConvert.SerializeObject(requestModel));

        try
        {
            var response = await _reservationManager.Create(requestModel);

            if (response.IsSuccess)
            {
                _logger.LogInformation("Create reservation finish: {Response}", JsonConvert.SerializeObject(response));

                return Ok(response);
            }

            _logger.LogError("Create reservation failed: {Response}", JsonConvert.SerializeObject(response));

            return BadRequest(new IsuErrorResponse(response.Exception.Message));
        }
        catch (Exception e)
        {
            _logger.LogError("Create reservation failed. Caught Exception: {Error}", JsonConvert.SerializeObject(e));

            return BadRequest(new IsuErrorResponse(MessageResource.UnhandledError));
        }
    }

    /// <summary>
    ///     Update a reservation
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
    /// <param name="reservationId"></param>
    /// <returns>Reservation updated</returns>
    [HttpPut]
    [Route("{reservationId:guid}")]
    [ProducesResponseType(typeof(IsuErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromForm] ReservationUpdateRequest requestModel,
        [FromRoute] Guid reservationId)
    {
        _logger.LogInformation("Update reservation using data: {Request}", JsonConvert.SerializeObject(requestModel));

        try
        {
            var response = await _reservationManager.Update(requestModel, reservationId);

            if (response.IsSuccess)
            {
                _logger.LogInformation("Update reservation finish: {Response}", JsonConvert.SerializeObject(response));

                return Ok(response);
            }

            _logger.LogError("Update reservation failed: {Response}", JsonConvert.SerializeObject(response));

            return BadRequest(new IsuErrorResponse(response.Exception.Message));
        }
        catch (Exception e)
        {
            _logger.LogError("Update reservation failed. Caught Exception: {Error}", JsonConvert.SerializeObject(e));

            return BadRequest(new IsuErrorResponse(MessageResource.UnhandledError));
        }
    }

    /// <summary>
    ///     Reservation list. This list is paginated
    /// </summary>
    /// <returns>Return a reservation list.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IsuErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Paging<ContactViewModel>>> List([FromQuery] string sortBy = "date",
        [FromQuery] bool sortDesc = false, [FromQuery] int page = 1, [FromQuery] int recordsPerPage = 10)
    {
        _logger.LogInformation("Get reservation list to show in table");

        try
        {
            var response = await _reservationManager.List(sortBy, sortDesc, page, recordsPerPage);

            if (response.IsSuccess)
            {
                _logger.LogInformation(
                    "Get reservation list to show in table finish: {Response}", JsonConvert.SerializeObject(response));

                return Ok(response);
            }

            _logger.LogError("Get reservation list to show in table failed: {Response}",
                JsonConvert.SerializeObject(response));

            return BadRequest(new IsuErrorResponse(response.Exception.Message));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "Get reservation list to show in table failed. Caught exception: {Error}",
                JsonConvert.SerializeObject(e));

            return BadRequest(new IsuErrorResponse(MessageResource.UnhandledError));
        }
    }
}