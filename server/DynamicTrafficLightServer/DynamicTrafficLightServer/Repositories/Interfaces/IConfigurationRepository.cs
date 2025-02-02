using DynamicTrafficLightServer.Models;

namespace DynamicTrafficLightServer.Repositories.Interfaces;

public interface IConfigurationRepository
{
    /// <summary>
    /// Retrieves all configurations from the database.
    /// Results are not tracked by the DbContext.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>List of all configurations.</returns>
    Task<List<Configuration>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a specific configuration by its ID.
    /// Results are tracked by the DbContext.
    /// </summary>
    /// <param name="id">The ID of the configuration to retrieve.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The configuration if found, otherwise null.</returns>
    Task<Configuration?> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a new configuration to the database.
    /// </summary>
    /// <param name="configuration">The configuration entity to add.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task AddAsync(Configuration configuration, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing configuration in the database.
    /// </summary>
    /// <param name="configuration">The configuration entity with updated values.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task UpdateAsync(Configuration configuration, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a configuration from the database by its ID.
    /// </summary>
    /// <param name="configuration">The configuration entity to delete.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task DeleteAsync(Configuration configuration, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a specific configuration by the traffic light ID.
    /// Results are not tracked by the DbContext.
    /// </summary>
    /// <param name="trafficLightId">The ID of the traffic light to retrieve the configuration for.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The configuration if found and active, otherwise null.</returns>
    Task<Configuration?> GetByTrafficLightIdAsync(int trafficLightId, CancellationToken cancellationToken);
}