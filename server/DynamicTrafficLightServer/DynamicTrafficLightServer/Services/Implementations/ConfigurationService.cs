using System.Net;
using DynamicTrafficLightServer.Dtos;
using DynamicTrafficLightServer.Enums;
using DynamicTrafficLightServer.Mappers;
using DynamicTrafficLightServer.Models;
using DynamicTrafficLightServer.Repositories.Interfaces;
using DynamicTrafficLightServer.Services.Interfaces;

namespace DynamicTrafficLightServer.Services.Implementations;

public class ConfigurationService(
    IConfigurationRepository configurationRepository,
    ITrafficLightRepository trafficLightRepository,
    IEntityChangeLogRepository changeLogRepository) : IConfigurationService
{
    /// <inheritdoc />
    public async Task<ServiceResponse<List<ConfigurationResponseModel>>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        var configurations = await configurationRepository.GetAllAsync(cancellationToken);

        return new ServiceResponse<List<ConfigurationResponseModel>>
        {
            Result = configurations.Select(ConfigurationMapper.ToResponseModel).ToList()
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<ConfigurationResponseModel>> GetByIdAsync(int id,
        CancellationToken cancellationToken)
    {
        var configuration = await configurationRepository.GetByIdAsync(id, cancellationToken);

        if (configuration is null)
        {
            return new ServiceResponse<ConfigurationResponseModel>
            {
                StatusCode = HttpStatusCode.NotFound,
                ErrorMessage = "Configuration not found."
            };
        }

        return new ServiceResponse<ConfigurationResponseModel>
        {
            Result = ConfigurationMapper.ToResponseModel(configuration)
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<ConfigurationResponseModel>> CreateAsync(
        ConfigurationRequestModel configurationRequestModel, CancellationToken cancellationToken)
    {
        var trafficLight =
            await trafficLightRepository.GetByIdAsync(configurationRequestModel.TrafficLightId, cancellationToken);

        if (trafficLight is null)
        {
            return new ServiceResponse<ConfigurationResponseModel>
            {
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessage = "Traffic Light not found."
            };
        }

        var configuration = ConfigurationMapper.ToModel(configurationRequestModel);

        configuration.CreatedById = 1; // TODO: Get from user context.
        configuration.CreateTime = DateTime.UtcNow;
        configuration.LastUpdatedById = 1; // TODO: Get from user context.
        configuration.LastUpdateTime = DateTime.UtcNow;

        await configurationRepository.AddAsync(configuration, cancellationToken);

        await changeLogRepository.AddAsync(new EntityChangeLog
        {
            EntityName = nameof(Configuration),
            EntityId = configuration.Id,
            Action = EntityChangeAction.Created,
            ChangedById = 1, // TODO: Get from user context.
            Timestamp = DateTime.UtcNow
        }, cancellationToken);

        return new ServiceResponse<ConfigurationResponseModel>
        {
            StatusCode = HttpStatusCode.Created,
            Result = ConfigurationMapper.ToResponseModel(configuration)
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<ConfigurationResponseModel>> UpdateAsync(int id,
        ConfigurationRequestModel configurationRequestModel, CancellationToken cancellationToken)
    {
        var configuration = await configurationRepository.GetByIdAsync(id, cancellationToken);

        if (configuration is null)
        {
            return new ServiceResponse<ConfigurationResponseModel>
            {
                StatusCode = HttpStatusCode.NotFound,
                ErrorMessage = "Configuration not found."
            };
        }

        if (configurationRequestModel.TrafficLightId != configuration.TrafficLightId)
        {
            var trafficLight =
                await trafficLightRepository.GetByIdAsync(configurationRequestModel.TrafficLightId, cancellationToken);

            if (trafficLight is null)
            {
                return new ServiceResponse<ConfigurationResponseModel>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessage = "Traffic Light not found."
                };
            }
        }

        configuration.TrafficLightId = configurationRequestModel.TrafficLightId;
        configuration.MinGreenTime = configurationRequestModel.MinGreenTime;
        configuration.MaxGreenTime = configurationRequestModel.MaxGreenTime;
        configuration.TimePerVehicle = configurationRequestModel.TimePerVehicle;
        configuration.SequenceGreenTime = configurationRequestModel.SequenceGreenTime;
        configuration.DefaultGreenTime = configurationRequestModel.DefaultGreenTime;
        configuration.DefaultRedTime = configurationRequestModel.DefaultRedTime;
        configuration.IsActive = configurationRequestModel.IsActive;

        configuration.LastUpdatedById = 1; // TODO: Get from user context.
        configuration.LastUpdateTime = DateTime.UtcNow;

        await configurationRepository.UpdateAsync(configuration, cancellationToken);

        await changeLogRepository.AddAsync(new EntityChangeLog
        {
            EntityName = nameof(Configuration),
            EntityId = configuration.Id,
            Action = EntityChangeAction.Updated,
            ChangedById = 1, // TODO: Get from user context.
            Timestamp = DateTime.UtcNow
        }, cancellationToken);

        return new ServiceResponse<ConfigurationResponseModel>
        {
            Result = ConfigurationMapper.ToResponseModel(configuration)
        };
    }

    /// <inheritdoc />
    public async Task<ServiceResponse<ConfigurationResponseModel>> DeleteAsync(int id,
        CancellationToken cancellationToken)
    {
        var configuration = await configurationRepository.GetByIdAsync(id, cancellationToken);

        if (configuration is null)
        {
            return new ServiceResponse<ConfigurationResponseModel>
            {
                StatusCode = HttpStatusCode.NotFound,
                ErrorMessage = "Configuration not found."
            };
        }

        await configurationRepository.DeleteAsync(configuration, cancellationToken);

        await changeLogRepository.AddAsync(new EntityChangeLog
        {
            EntityName = nameof(Configuration),
            EntityId = configuration.Id,
            Action = EntityChangeAction.Deleted,
            ChangedById = 1, // TODO: Get from user context.
            Timestamp = DateTime.UtcNow
        }, cancellationToken);

        return new ServiceResponse<ConfigurationResponseModel>();
    }
}