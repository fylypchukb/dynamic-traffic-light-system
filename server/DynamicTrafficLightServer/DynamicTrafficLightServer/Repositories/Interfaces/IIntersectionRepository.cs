using DynamicTrafficLightServer.Models;

namespace DynamicTrafficLightServer.Repositories.Interfaces;

public interface IIntersectionRepository
{
    /// <summary>
    /// Retrieves all intersections from the database.
    /// Results are not tracked by the DbContext.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>List of all intersections.</returns>
    Task<List<Intersection>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a specific intersection by its ID.
    /// Results are tracked by the DbContext.
    /// </summary>
    /// <param name="id">The ID of the intersection to retrieve.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The intersection if found, otherwise null.</returns>
    Task<Intersection?> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    ///  Adds a new intersection to the database.
    /// </summary>
    /// <param name="intersection">The intersection entity to add.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task AddAsync(Intersection intersection, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing intersection in the database.
    /// </summary>
    /// <param name="intersection">The intersection entity with updated values.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task UpdateAsync(Intersection intersection, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes an intersection from the database by its ID.
    /// </summary>
    /// <param name="intersection">The intersection entity to delete.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    Task DeleteAsync(Intersection intersection, CancellationToken cancellationToken);
}