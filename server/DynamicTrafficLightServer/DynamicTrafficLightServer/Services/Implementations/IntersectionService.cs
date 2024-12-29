using System.Net;
using DynamicTrafficLightServer.Dtos;
using DynamicTrafficLightServer.Mappers;
using DynamicTrafficLightServer.Repositories.Interfaces;
using DynamicTrafficLightServer.Services.Interfaces;

namespace DynamicTrafficLightServer.Services.Implementations;

public class IntersectionService(IIntersectionRepository intersectionRepository) : IIntersectionService
{
    /// <inheritdoc />
    public async Task<ServiceResponse<List<IntersectionResponseModel>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var intersections = await intersectionRepository.GetAllAsync(cancellationToken);

        return new ServiceResponse<List<IntersectionResponseModel>>
        {
            Result = intersections.Select(IntersectionMapper.ToResponseModel).ToList()
        };
    }

    public Task<ServiceResponse<IntersectionResponseModel?>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<IntersectionResponseModel>> CreateAsync(
        IntersectionRequestModel intersectionRequestModel, CancellationToken cancellationToken)
    {
        var intersection = IntersectionMapper.ToModel(intersectionRequestModel);
        
        await intersectionRepository.AddAsync(intersection, cancellationToken);
        
        return new ServiceResponse<IntersectionResponseModel>
        {
            StatusCode = HttpStatusCode.Created,
            Result = IntersectionMapper.ToResponseModel(intersection)
        };
    }

    public Task<ServiceResponse<IntersectionResponseModel>> UpdateAsync(int id,
        IntersectionRequestModel intersectionRequestModel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}