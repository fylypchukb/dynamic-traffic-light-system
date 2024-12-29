using System.Net;
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

        return result.StatusCode == HttpStatusCode.Created
            ? Created($"/api/v1/intersection/{result.Result!.Id}", result.ToApiResponse())
            : StatusCode((int)result.StatusCode, result.ToApiResponse());
    }
}