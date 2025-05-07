using DynamicTrafficLightServer.Dtos.EntityChangeLogDto;
using DynamicTrafficLightServer.Models;

namespace DynamicTrafficLightServer.Repositories.Interfaces;

public interface IEntityChangeLogRepository
{
    /// <summary>
    /// Retrieves a filtered list of <see cref="EntityChangeLog"/> entries based on the specified filter criteria.
    /// </summary>
    /// <param name="filter">The filter object containing criteria.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A list of matching <see cref="EntityChangeLog"/> records.</returns>
    Task<List<EntityChangeLog>> GetFilteredAsync(EntityChangeLogFilterRequestModel filter,
        CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a specific entity change log by its ID.
    /// </summary>
    /// <param name="id">The ID of the change log entry.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>The <see cref="EntityChangeLog"/> entry if found; otherwise, <c>null</c>.</returns>
    Task<EntityChangeLog?> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a new entity change log entry to the database and saves the changes.
    /// </summary>
    /// <param name="log">The change log entry to add.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddAsync(EntityChangeLog log, CancellationToken cancellationToken);
}