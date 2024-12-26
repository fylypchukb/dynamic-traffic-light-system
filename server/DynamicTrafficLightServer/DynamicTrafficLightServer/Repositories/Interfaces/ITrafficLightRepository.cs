using DynamicTrafficLightServer.Models;

namespace DynamicTrafficLightServer.Repositories.Interfaces;

public interface ITrafficLightRepository
{
    /// <summary>
    ///     Retrieves all traffic lights from the database.
    ///     Results are not tracked by the DbContext.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>List of all traffic lights.</returns>
    Task<List<TrafficLight>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Retrieves a specific traffic light by its ID.
    ///     Results are tracked by the DbContext.
    /// </summary>
    /// <param name="id">The ID of the traffic light to retrieve.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The traffic light if found, otherwise null.</returns>
    Task<TrafficLight?> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    ///     Adds a new traffic light to the database.
    /// </summary>
    /// <param name="trafficLight">The traffic light entity to add.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task AddAsync(TrafficLight trafficLight, CancellationToken cancellationToken);

    /// <summary>
    ///     Updates an existing traffic light in the database.
    /// </summary>
    /// <param name="trafficLight">The traffic light entity with updated values.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task UpdateAsync(TrafficLight trafficLight, CancellationToken cancellationToken);

    /// <summary>
    ///     Deletes a traffic light from the database by its ID.
    /// </summary>
    /// <param name="trafficLight">The traffic light entity to delete.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task DeleteAsync(TrafficLight trafficLight, CancellationToken cancellationToken);
}