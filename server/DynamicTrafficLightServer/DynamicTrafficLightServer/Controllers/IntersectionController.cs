using DynamicTrafficLightServer.Dtos;
using DynamicTrafficLightServer.Mappers;
using DynamicTrafficLightServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DynamicTrafficLightServer.Controllers;

/// <summary>
/// Controller for managing intersections.
/// </summary>
/// <param name="intersectionService">Service for handling intersection operations.</param>
[ApiController]
[Route("api/v1/[controller]")]
public class IntersectionController(IIntersectionService intersectionService) : ControllerBase
{
    /// <summary>
    /// Retrieve all.
    /// </summary>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>
    /// A list of <see cref="IntersectionResponseModel"/>.
    /// </returns>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<IntersectionResponseModel>>>> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await intersectionService.GetAllAsync(cancellationToken);

        return result.ToApiResponse();
    }

    /// <summary>
    /// Retrieve by id.
    /// </summary>
    /// <param name="id">The identifier of the intersection.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>
    /// The <see cref="IntersectionResponseModel"/>.
    /// </returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<IntersectionResponseModel>>> GetById(int id,
        CancellationToken cancellationToken)
    {
        var result = await intersectionService.GetByIdAsync(id, cancellationToken);

        return StatusCode((int)result.StatusCode, result.ToApiResponse());
    }

    /// <summary>
    /// Create a new intersection.
    /// </summary>
    /// <param name="intersectionRequestModel">The request model containing intersection details.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>
    /// The created <see cref="IntersectionResponseModel"/>.
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<IntersectionResponseModel>>> Create(
        IntersectionRequestModel intersectionRequestModel, CancellationToken cancellationToken)
    {
        var result = await intersectionService.CreateAsync(intersectionRequestModel, cancellationToken);

        return result.ToCreatedApiResponse("/api/v1/intersection");
    }

    /// <summary>
    /// Update an intersection.
    /// </summary>
    /// <param name="id">The identifier of the intersection to update.</param>
    /// <param name="intersectionRequestModel">The request model containing updated intersection details.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>
    /// The updated <see cref="IntersectionResponseModel"/>.
    /// </returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<IntersectionResponseModel>>> Update(int id,
        IntersectionRequestModel intersectionRequestModel, CancellationToken cancellationToken)
    {
        var result = await intersectionService.UpdateAsync(id, intersectionRequestModel, cancellationToken);

        return result.ToApiResponse();
    }

    /// <summary>
    /// Delete an intersection.
    /// </summary>
    /// <param name="id">The identifier of the intersection to delete.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<IntersectionResponseModel>>> Delete(int id,
        CancellationToken cancellationToken)
    {
        var result = await intersectionService.DeleteAsync(id, cancellationToken);

        return result.ToApiResponse();
    }
}