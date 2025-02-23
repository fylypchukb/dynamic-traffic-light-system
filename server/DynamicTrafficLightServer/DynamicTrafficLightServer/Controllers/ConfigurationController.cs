using DynamicTrafficLightServer.Dtos;
using DynamicTrafficLightServer.Mappers;
using DynamicTrafficLightServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DynamicTrafficLightServer.Controllers;

/// <summary>
/// Controller for managing configurations.
/// </summary>
/// <param name="configurationService">Service for handling configuration operations.</param>
[ApiController]
[Route("api/v1/[controller]")]
public class ConfigurationController(IConfigurationService configurationService) : ControllerBase
{
    /// <summary>
    /// Retrieves all configurations.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A list of configuration response models.</returns>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<ConfigurationResponseModel>>>> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await configurationService.GetAllAsync(cancellationToken);

        return result.ToApiResponse();
    }

    /// <summary>
    /// Retrieves a specific configuration by its ID.
    /// </summary>
    /// <param name="id">The ID of the configuration to retrieve.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The configuration response model if found, otherwise an error message.</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<ConfigurationResponseModel>>> GetById(int id,
        CancellationToken cancellationToken)
    {
        var result = await configurationService.GetByIdAsync(id, cancellationToken);

        return result.ToApiResponse();
    }

    /// <summary>
    /// Creates a new configuration.
    /// </summary>
    /// <param name="configurationRequestModel">The configuration request model containing the data to create the configuration.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The created configuration response model.</returns>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<ConfigurationResponseModel>>> Create(
        ConfigurationRequestModel configurationRequestModel, CancellationToken cancellationToken)
    {
        var result = await configurationService.CreateAsync(configurationRequestModel, cancellationToken);

        return result.ToCreatedApiResponse("/api/v1/configuration");
    }

    /// <summary>
    /// Updates an existing configuration.
    /// </summary>
    /// <param name="id">The ID of the configuration to update.</param>
    /// <param name="configurationRequestModel">The configuration request model containing the updated data.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The updated configuration response model.</returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<ConfigurationResponseModel>>> Update(int id,
        ConfigurationRequestModel configurationRequestModel, CancellationToken cancellationToken)
    {
        var result = await configurationService.UpdateAsync(id, configurationRequestModel, cancellationToken);

        return result.ToApiResponse();
    }

    /// <summary>
    /// Deletes a configuration by its ID.
    /// </summary>
    /// <param name="id">The ID of the configuration to delete.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The result of the delete operation.</returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<ConfigurationResponseModel>>> Delete(int id,
        CancellationToken cancellationToken)
    {
        var result = await configurationService.DeleteAsync(id, cancellationToken);

        return result.ToApiResponse();
    }
}