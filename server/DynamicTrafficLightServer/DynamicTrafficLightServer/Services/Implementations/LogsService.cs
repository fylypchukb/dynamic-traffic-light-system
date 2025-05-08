using DynamicTrafficLightServer.Dtos;
using DynamicTrafficLightServer.Mappers;
using DynamicTrafficLightServer.Repositories.Interfaces;
using DynamicTrafficLightServer.Services.Interfaces;

namespace DynamicTrafficLightServer.Services.Implementations;

public class LogsService(
    IEntityChangeLogRepository entityChangeLogRepository,
    ITrafficSwitchLogRepository switchLogRepository) : ILogsService
{
    public async Task<ServiceResponse<List<EntityChangeLogResponseModel>>> GetFilteredEntityChangeLogs(
        EntityChangeLogFilterModel filterModel, CancellationToken cancellationToken)
    {
        var results = await entityChangeLogRepository.GetFilteredAsync(filterModel, cancellationToken);

        return new ServiceResponse<List<EntityChangeLogResponseModel>>
        {
            Result = results.Select(EntityChangeLogMapper.ToResponseModel).ToList()
        };
    }

    public async Task<ServiceResponse<List<TrafficSwitchLogResponseModel>>> GetFilteredTrafficLightSwitchLogs(
        TrafficSwitchLogFilterModel filterModel,
        CancellationToken cancellationToken)
    {
        var results = await switchLogRepository.GetFilteredAsync(filterModel, cancellationToken);

        return new ServiceResponse<List<TrafficSwitchLogResponseModel>>
        {
            Result = results.Select(TrafficSwitchLogMapper.ToResponseModel).ToList()
        };
    }
}