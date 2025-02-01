using System.Net;
using DynamicTrafficLightServer.Dtos;
using DynamicTrafficLightServer.Mappers;
using DynamicTrafficLightServer.Repositories.Interfaces;
using DynamicTrafficLightServer.Services.Interfaces;

namespace DynamicTrafficLightServer.Services.Implementations;

public class TrafficLightService(
    ITrafficLightRepository trafficLightRepository,
    IIntersectionRepository intersectionRepository) : ITrafficLightService
{
    /// <inheritdoc />
    public async Task<ServiceResponse<List<TrafficLightResponseModel>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var trafficLights = await trafficLightRepository.GetAllAsync(cancellationToken);

        return new ServiceResponse<List<TrafficLightResponseModel>>
        {
            Result = trafficLights.Select(TrafficLightMapper.ToResponseModel).ToList()
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<TrafficLightResponseModel>> GetByIdAsync(int id,
        CancellationToken cancellationToken)
    {
        var trafficLight = await trafficLightRepository.GetByIdAsync(id, cancellationToken);

        if (trafficLight is null)
        {
            return new ServiceResponse<TrafficLightResponseModel>
            {
                StatusCode = HttpStatusCode.NotFound,
                ErrorMessage = "Traffic Light not found."
            };
        }

        return new ServiceResponse<TrafficLightResponseModel>
        {
            Result = TrafficLightMapper.ToResponseModel(trafficLight)
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<TrafficLightResponseModel>> CreateAsync(
        TrafficLightRequestModel trafficLightRequestModel, CancellationToken cancellationToken)
    {
        var intersection =
            await intersectionRepository.GetByIdAsync(trafficLightRequestModel.IntersectionId, cancellationToken);

        if (intersection is null)
        {
            return new ServiceResponse<TrafficLightResponseModel>
            {
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessage = "Intersection not found."
            };
        }

        var trafficLight = TrafficLightMapper.ToModel(trafficLightRequestModel);

        trafficLight.CreatedById = 1; // TODO: Get from user context.
        trafficLight.CreateTime = DateTime.UtcNow;
        trafficLight.LastUpdatedById = 1; // TODO: Get from user context.
        trafficLight.LastUpdateTime = DateTime.UtcNow;

        await trafficLightRepository.AddAsync(trafficLight, cancellationToken);

        return new ServiceResponse<TrafficLightResponseModel>
        {
            StatusCode = HttpStatusCode.Created,
            Result = TrafficLightMapper.ToResponseModel(trafficLight)
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<TrafficLightResponseModel>> UpdateAsync(int id,
        TrafficLightRequestModel trafficLightRequestModel, CancellationToken cancellationToken)
    {
        var trafficLight = await trafficLightRepository.GetByIdAsync(id, cancellationToken);

        if (trafficLight is null)
        {
            return new ServiceResponse<TrafficLightResponseModel>
            {
                StatusCode = HttpStatusCode.NotFound,
                ErrorMessage = "Traffic Light not found."
            };
        }

        if (trafficLight.IntersectionId != trafficLightRequestModel.IntersectionId)
        {
            var intersection =
                await intersectionRepository.GetByIdAsync(trafficLightRequestModel.IntersectionId, cancellationToken);
            
            if (intersection is null)
            {
                return new ServiceResponse<TrafficLightResponseModel>
                {
                    StatusCode = HttpStatusCode.UnprocessableEntity,
                    ErrorMessage = "Intersection not found."
                };
            }
        }

        trafficLight.Name = trafficLightRequestModel.Name;
        trafficLight.IntersectionId = trafficLightRequestModel.IntersectionId;
        trafficLight.Priority = trafficLightRequestModel.Priority;
        trafficLight.IsActive = trafficLightRequestModel.IsActive;
        trafficLight.LastUpdatedById = 1; // TODO: Get from user context.
        trafficLight.LastUpdateTime = DateTime.UtcNow;

        await trafficLightRepository.UpdateAsync(trafficLight, cancellationToken);

        return new ServiceResponse<TrafficLightResponseModel>
        {
            Result = TrafficLightMapper.ToResponseModel(trafficLight)
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<TrafficLightResponseModel>> DeleteAsync(int id,
        CancellationToken cancellationToken)
    {
        var trafficLight = await trafficLightRepository.GetByIdAsync(id, cancellationToken);

        if (trafficLight is null)
        {
            return new ServiceResponse<TrafficLightResponseModel>
            {
                StatusCode = HttpStatusCode.NotFound,
                ErrorMessage = "Traffic Light not found."
            };
        }

        await trafficLightRepository.DeleteAsync(trafficLight, cancellationToken);

        return new ServiceResponse<TrafficLightResponseModel>();
    }
}