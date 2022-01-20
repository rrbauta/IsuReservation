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

    /// <summary>
    ///     Destination as favorite
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="destinationId"></param>
    /// <returns></returns>
    [Route("favorite/{destinationId:guid}")]
    [HttpPut]
    [ProducesResponseType(typeof(IsuErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SetFavorite([FromRoute] Guid destinationId)
    {
        _logger.LogInformation("Destination as favorite using data: offerId {ContactId}", destinationId);
        try
        {
            var response = await _destinationManager.SetFavorite(destinationId);

            if (response.IsSuccess)
            {
                _logger.LogInformation("Destination as favorite finish: {Response}",
                    JsonConvert.SerializeObject(response));

                return Ok(response);
            }

            _logger.LogError("Destination as favorite failed: {Response}", JsonConvert.SerializeObject(response));

            return BadRequest(new IsuErrorResponse(response.Exception.Message));
        }
        catch (Exception e)
        {
            _logger.LogError("Destination as favorite failed. Caught Exception: {Error}",
                JsonConvert.SerializeObject(e));

            return BadRequest(new IsuErrorResponse(MessageResource.UnhandledError));
        }
    }

    /// <summary>
    ///     Set destination ranking
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="destinationId"></param>
    /// <param name="requestModel"></param>
    /// <returns></returns>
    [Route("ranking/{destinationId:guid}")]
    [HttpPut]
    [ProducesResponseType(typeof(IsuErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SetRanking([FromRoute] Guid destinationId,
        [FromBody] SetRankingRequest requestModel)
    {
        _logger.LogInformation("Set destination ranking using data: offerId {DestinationId} and model: {Model}",
            destinationId, JsonConvert.SerializeObject(requestModel));
        try
        {
            var response = await _destinationManager.SetRanking(destinationId, requestModel);

            if (response.IsSuccess)
            {
                _logger.LogInformation("Set destination ranking finish: {Response}",
                    JsonConvert.SerializeObject(response));

                return Ok(response);
            }

            _logger.LogError("Set destination ranking failed: {Response}", JsonConvert.SerializeObject(response));

            return BadRequest(new IsuErrorResponse(response.Exception.Message));
        }
        catch (Exception e)
        {
            _logger.LogError("Set destination ranking failed. Caught Exception: {Error}",
                JsonConvert.SerializeObject(e));

            return BadRequest(new IsuErrorResponse(MessageResource.UnhandledError));
        }
    }
}