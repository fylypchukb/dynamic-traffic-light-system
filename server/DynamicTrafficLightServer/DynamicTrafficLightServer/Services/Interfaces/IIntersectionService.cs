using DynamicTrafficLightServer.Dtos;

namespace DynamicTrafficLightServer.Services.Interfaces;

public interface IIntersectionService
{
    /// <summary>
    /// Asynchronously retrieves all intersection response models.
    /// </summary>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>
    /// A list of <see cref="IntersectionResponseModel"/>.
    /// </returns>
    Task<ServiceResponse<List<IntersectionResponseModel>>> GetAllAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Asynchronously retrieves an intersection by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the intersection.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>
    /// The <see cref="IntersectionResponseModel"/>.
    /// </returns>
    Task<ServiceResponse<IntersectionResponseModel>> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously creates a new intersection.
    /// </summary>
    /// <param name="intersectionRequestModel">The request model containing intersection details.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>
    /// The created <see cref="IntersectionResponseModel"/>.
    /// </returns>
    Task<ServiceResponse<IntersectionResponseModel>> CreateAsync(IntersectionRequestModel intersectionRequestModel,
        CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously updates an existing intersection.
    /// </summary>
    /// <param name="id">The identifier of the intersection to update.</param>
    /// <param name="intersectionRequestModel">The request model containing updated intersection details.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>
    /// The updated <see cref="IntersectionResponseModel"/>.
    /// </returns>
    Task<ServiceResponse<IntersectionResponseModel>> UpdateAsync(int id,
        IntersectionRequestModel intersectionRequestModel, CancellationToken cancellationToken);
    
    /// <summary>
    /// Asynchronously deletes an intersection by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the intersection to delete.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>
    /// The result of the delete operation.
    /// </returns>
    Task<ServiceResponse<IntersectionResponseModel>> DeleteAsync(int id, CancellationToken cancellationToken);
}