using DynamicTrafficLightServer.Models;

namespace DynamicTrafficLightServer.Repositories.Interfaces;

public interface IEntityChangeLogRepository
{
    /// <summary>
    /// Retrieves all entity change logs from the database.
    /// </summary>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A list of all <see cref="EntityChangeLog"/> entries.</returns>
    Task<List<EntityChangeLog>> GetAllAsync(CancellationToken cancellationToken);

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