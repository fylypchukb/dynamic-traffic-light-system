using DynamicTrafficLightServer.Dtos;
using DynamicTrafficLightServer.Mappers;
using DynamicTrafficLightServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DynamicTrafficLightServer.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class LogsController(ILogsService logsService) : ControllerBase
{
    /// <summary>
    /// Gets filtered entity change logs.
    /// </summary>
    /// <param name="filter">Filter by name, ID, action, or date range.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of entity change logs.</returns>
    [HttpPost("entity-changes")]
    public async Task<ActionResult<ApiResponse<List<EntityChangeLogResponseModel>>>> GetFilteredEntityChangeLogs(
        EntityChangeLogFilterModel filter, CancellationToken cancellationToken)
    {
        var result = await logsService.GetFilteredEntityChangeLogs(filter, cancellationToken);

        return result.ToApiResponse();
    }

    /// <summary>
    /// Gets filtered traffic light switch logs.
    /// </summary>
    /// <param name="filter">Filter by traffic light ID or date range.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of traffic light switch logs.</returns>
    [HttpPost("traffic-light-logs")]
    public async Task<ActionResult<ApiResponse<List<TrafficSwitchLogResponseModel>>>> GetFilteredTrafficLightSwitchLogs(
        TrafficSwitchLogFilterModel filter, CancellationToken cancellationToken)
    {
        var result = await logsService.GetFilteredTrafficLightSwitchLogs(filter, cancellationToken);

        return result.ToApiResponse();
    }
}