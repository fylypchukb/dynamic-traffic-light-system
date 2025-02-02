using System.Net;
using DynamicTrafficLightServer.Dtos;
using DynamicTrafficLightServer.Mappers;
using DynamicTrafficLightServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DynamicTrafficLightServer.Controllers;

/// <summary>
/// Controller for managing traffic lights.
/// </summary>
/// <param name="trafficLightService">Service for handling traffic lights operations.</param>
[ApiController]
[Route("api/v1/[controller]")]
public class TrafficLightController(ITrafficLightService trafficLightService) : ControllerBase
{
    /// <summary>
    /// Retrieve all traffic lights.
    /// </summary>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>
    /// A list of <see cref="TrafficLightResponseModel"/>.
    /// </returns>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<TrafficLightResponseModel>>>> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await trafficLightService.GetAllAsync(cancellationToken);

        return StatusCode((int)result.StatusCode, result.ToApiResponse());
    }

    /// <summary>
    /// Retrieve a traffic light by id.
    /// </summary>
    /// <param name="id">The identifier of the traffic light.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>
    /// The <see cref="TrafficLightResponseModel"/>.
    /// </returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<TrafficLightResponseModel>>> GetById(int id,
        CancellationToken cancellationToken)
    {
        var result = await trafficLightService.GetByIdAsync(id, cancellationToken);

        return StatusCode((int)result.StatusCode, result.ToApiResponse());
    }

    /// <summary>
    /// Create a new traffic light.
    /// </summary>
    /// <param name="trafficLightRequestModel">The request model containing traffic light details.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>
    /// The created <see cref="TrafficLightResponseModel"/>.
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<TrafficLightResponseModel>>> Create(
        TrafficLightRequestModel trafficLightRequestModel, CancellationToken cancellationToken)
    {
        var result = await trafficLightService.CreateAsync(trafficLightRequestModel, cancellationToken);

        return result.StatusCode == HttpStatusCode.Created
            ? Created($"/api/v1/trafficLight/{result.Result!.Id}", result.ToApiResponse())
            : StatusCode((int)result.StatusCode, result.ToApiResponse());
    }

    /// <summary>
    /// Update a traffic light.
    /// </summary>
    /// <param name="id">The identifier of the traffic light to update.</param>
    /// <param name="trafficLightRequestModel">The request model containing updated traffic light details.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>
    /// The updated <see cref="TrafficLightResponseModel"/>.
    /// </returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<TrafficLightResponseModel>>> Update(int id,
        TrafficLightRequestModel trafficLightRequestModel, CancellationToken cancellationToken)
    {
        var result = await trafficLightService.UpdateAsync(id, trafficLightRequestModel, cancellationToken);

        return StatusCode((int)result.StatusCode, result.ToApiResponse());
    }

    /// <summary>
    /// Delete a traffic light.
    /// </summary>
    /// <param name="id">The identifier of the traffic light to delete.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<TrafficLightResponseModel>>> Delete(int id,
        CancellationToken cancellationToken)
    {
        var result = await trafficLightService.DeleteAsync(id, cancellationToken);

        return StatusCode((int)result.StatusCode, result.ToApiResponse());
    }
}