using DynamicTrafficLightServer.Dtos;
using DynamicTrafficLightServer.Mappers;
using DynamicTrafficLightServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DynamicTrafficLightServer.Controllers;

/// <summary>
/// Controller for managing traffic flow operations.
/// </summary>
/// <param name="trafficFlowService">Service for handling traffic flow operations.</param>
[ApiController]
[Route("api/v1/[controller]")]
public class TrafficFlowController(ITrafficFlowService trafficFlowService) : ControllerBase
{
    /// <summary>
    /// Calculate the green light duration.
    /// </summary>
    /// <param name="trafficDataRequest">The traffic data request containing the traffic light ID and the number of cars.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The calculated green light duration.</returns>
    [HttpPost("calculate-green-light")]
    public async Task<ActionResult<ApiResponse<TrafficDataResponse>>> CalculateGreenLight(
        [FromBody] TrafficDataRequest trafficDataRequest,
        CancellationToken cancellationToken)
    {
        var result = await trafficFlowService.CalculateGreenLightAsync(trafficDataRequest, cancellationToken);

        return StatusCode((int)result.StatusCode, result.ToApiResponse());
    }
}