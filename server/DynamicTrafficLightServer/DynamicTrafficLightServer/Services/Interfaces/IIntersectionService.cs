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
    Task<ServiceResponse<IntersectionResponseModel?>> GetByIdAsync(int id, CancellationToken cancellationToken);

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

    Task<ServiceResponse<IntersectionResponseModel>> UpdateAsync(int id,
        IntersectionRequestModel intersectionRequestModel, CancellationToken cancellationToken);
}