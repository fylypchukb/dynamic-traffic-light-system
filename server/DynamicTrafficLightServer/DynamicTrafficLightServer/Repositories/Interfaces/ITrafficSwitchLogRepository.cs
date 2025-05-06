using DynamicTrafficLightServer.Models;

namespace DynamicTrafficLightServer.Repositories.Interfaces;

public interface ITrafficSwitchLogRepository
{
    /// <summary>
    /// Retrieves all traffic switch logs from the database.
    /// </summary>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A list of all <see cref="TrafficSwitchLog"/> entries.</returns>
    Task<List<TrafficSwitchLog>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a specific traffic switch log by its ID.
    /// </summary>
    /// <param name="id">The ID of the switch log entry.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>The <see cref="TrafficSwitchLog"/> entry if found; otherwise, <c>null</c>.</returns>
    Task<TrafficSwitchLog?> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a new traffic switch log entry to the database.
    /// </summary>
    /// <param name="log">The switch log entry to add.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddAsync(TrafficSwitchLog log, CancellationToken cancellationToken);
}