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

    /// <inheritdoc />
    public async Task<ServiceResponse<IntersectionResponseModel>> GetByIdAsync(int id,
        CancellationToken cancellationToken)
    {
        var intersection = await intersectionRepository.GetByIdAsync(id, cancellationToken);

        if (intersection is null)
        {
            return new ServiceResponse<IntersectionResponseModel>
            {
                StatusCode = HttpStatusCode.NotFound,
                ErrorMessage = "Intersection not found."
            };
        }

        return new ServiceResponse<IntersectionResponseModel>
        {
            Result = IntersectionMapper.ToResponseModel(intersection)
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<IntersectionResponseModel>> CreateAsync(
        IntersectionRequestModel intersectionRequestModel, CancellationToken cancellationToken)
    {
        var intersection = IntersectionMapper.ToModel(intersectionRequestModel);

        intersection.CreatedById = 1; // TODO: Get from user context.
        intersection.CreateTime = DateTime.UtcNow;
        intersection.LastUpdatedById = 1; // TODO: Get from user context.
        intersection.LastUpdateTime = DateTime.UtcNow;

        await intersectionRepository.AddAsync(intersection, cancellationToken);

        return new ServiceResponse<IntersectionResponseModel>
        {
            StatusCode = HttpStatusCode.Created,
            Result = IntersectionMapper.ToResponseModel(intersection)
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<IntersectionResponseModel>> UpdateAsync(int id,
        IntersectionRequestModel intersectionRequestModel, CancellationToken cancellationToken)
    {
        var intersection = await intersectionRepository.GetByIdAsync(id, cancellationToken);

        if (intersection is null)
        {
            return new ServiceResponse<IntersectionResponseModel>
            {
                StatusCode = HttpStatusCode.NotFound,
                ErrorMessage = "Intersection not found."
            };
        }

        intersection.City = intersectionRequestModel.City;
        intersection.Location = intersectionRequestModel.Location;
        intersection.LastUpdateTime = DateTime.UtcNow;
        intersection.LastUpdatedById = 1; // TODO: Get from user context.
        intersection.IsActive = intersectionRequestModel.IsActive;

        await intersectionRepository.UpdateAsync(intersection, cancellationToken);

        return new ServiceResponse<IntersectionResponseModel>
        {
            Result = IntersectionMapper.ToResponseModel(intersection)
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<IntersectionResponseModel>> DeleteAsync(int id,
        CancellationToken cancellationToken)
    {
        var intersection = await intersectionRepository.GetByIdAsync(id, cancellationToken);

        if (intersection is null)
        {
            return new ServiceResponse<IntersectionResponseModel>
            {
                StatusCode = HttpStatusCode.NotFound,
                ErrorMessage = "Intersection not found."
            };
        }

        await intersectionRepository.DeleteAsync(intersection, cancellationToken);

        return new ServiceResponse<IntersectionResponseModel>();
    }
}