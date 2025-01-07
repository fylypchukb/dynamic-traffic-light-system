using DynamicTrafficLightServer.Dtos;

namespace DynamicTrafficLightServer.Services.Interfaces;

/// <summary>
/// Service interface for managing traffic lights.
/// </summary>
public interface ITrafficLightService
{
    /// <summary>
    /// Retrieves all traffic lights.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A service response containing a list of traffic light response models.</returns>
    Task<ServiceResponse<List<TrafficLightResponseModel>>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a specific traffic light by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the traffic light.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A service response containing the traffic light response model.</returns>
    Task<ServiceResponse<TrafficLightResponseModel>> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new traffic light.
    /// </summary>
    /// <param name="trafficLightRequestModel">The model containing the details of the traffic light to create.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A service response containing the created traffic light response model.</returns>
    Task<ServiceResponse<TrafficLightResponseModel>> CreateAsync(TrafficLightRequestModel trafficLightRequestModel,
        CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing traffic light.
    /// </summary>
    /// <param name="id">The unique identifier of the traffic light to update.</param>
    /// <param name="trafficLightRequestModel">The model containing the updated details of the traffic light.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A service response containing the updated traffic light response model.</returns>
    Task<ServiceResponse<TrafficLightResponseModel>> UpdateAsync(int id,
        TrafficLightRequestModel trafficLightRequestModel, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a traffic light.
    /// </summary>
    /// <param name="id">The unique identifier of the traffic light to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A service response indicating the result of the deletion operation.</returns>
    Task<ServiceResponse<TrafficLightResponseModel>> DeleteAsync(int id, CancellationToken cancellationToken);
}