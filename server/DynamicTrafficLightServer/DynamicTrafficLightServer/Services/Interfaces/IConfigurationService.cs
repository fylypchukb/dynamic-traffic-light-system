using DynamicTrafficLightServer.Dtos;

namespace DynamicTrafficLightServer.Services.Interfaces;

public interface IConfigurationService
{
    /// <summary>
    /// Retrieves all configurations asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A list of configuration response models.</returns>
    public Task<ServiceResponse<List<ConfigurationResponseModel>>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a specific configuration by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the configuration to retrieve.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The configuration response model if found, otherwise an error message.</returns>
    public Task<ServiceResponse<ConfigurationResponseModel>> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new configuration asynchronously.
    /// </summary>
    /// <param name="configurationRequestModel">The configuration request model containing the data to create the configuration.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The created configuration response model.</returns>
    public Task<ServiceResponse<ConfigurationResponseModel>> CreateAsync(
        ConfigurationRequestModel configurationRequestModel, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing configuration asynchronously.
    /// </summary>
    /// <param name="id">The ID of the configuration to update.</param>
    /// <param name="configurationRequestModel">The configuration request model containing the updated data.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The updated configuration response model.</returns>
    public Task<ServiceResponse<ConfigurationResponseModel>> UpdateAsync(int id,
        ConfigurationRequestModel configurationRequestModel, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a configuration by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the configuration to delete.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The result of the delete operation.</returns>
    public Task<ServiceResponse<ConfigurationResponseModel>> DeleteAsync(int id, CancellationToken cancellationToken);
}