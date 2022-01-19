using IsuReservation.Abstract;
using IsuReservation.Models.Response;
using IsuReservation.Models.ViewModel;
using IsuReservation.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IsuReservation.Controllers;

/// <summary>
/// </summary>
[Route("api/destinations")]
[ApiController]
public class DestinationController : ControllerBase
{
    private readonly IDestinationManager _destinationManager;
    private readonly ILogger<DestinationController> _logger;

    /// <summary>
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="destinationManager"></param>
    public DestinationController(ILogger<DestinationController> logger, IDestinationManager destinationManager)
    {
        _logger = logger;
        _destinationManager = destinationManager;
    }

    /// <summary>
    ///     Destinations list to show in reservation form
    /// </summary>
    /// <returns>Return a destinations list.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IsuErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Paging<ContactViewModel>>> List()
    {
        _logger.LogInformation("Destinations list to show in reservation form");

        try
        {
            var response = await _destinationManager.List();

            if (response.IsSuccess)
            {
                _logger.LogInformation(
                    "Destinations list to show in reservation form finish: {Response}",
                    JsonConvert.SerializeObject(response));

                return Ok(response);
            }

            _logger.LogError(
                "Destinations list to show in reservation form failed: {Response}",
                JsonConvert.SerializeObject(response));

            return BadRequest(new IsuErrorResponse(response.Exception.Message));
        }
        catch (Exception e)
        {
            _logger.LogError(
                "Destinations list to show in reservation form failed. Caught exception: {Error}",
                JsonConvert.SerializeObject(e));

            return BadRequest(new IsuErrorResponse(MessageResource.UnhandledError));
        }
    }
}