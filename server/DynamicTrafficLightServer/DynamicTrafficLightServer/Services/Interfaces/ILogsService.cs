using DynamicTrafficLightServer.Dtos;
using DynamicTrafficLightServer.Dtos.EntityChangeLogDto;
using DynamicTrafficLightServer.Dtos.TrafficSwitchLogDto;

namespace DynamicTrafficLightServer.Services.Interfaces;

public interface ILogsService
{
    /// <summary>
    /// Retrieves a filtered list of entity change logs and maps them to response models.
    /// </summary>
    /// <param name="filterModel">Filtering criteria such as entity name, ID, action, and date range.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A service response containing the list of matching <see cref="EntityChangeLogResponseModel"/> records.</returns>
    Task<ServiceResponse<List<EntityChangeLogResponseModel>>> GetFilteredEntityChangeLogs(
        EntityChangeLogFilterRequestModel filterModel, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a filtered list of traffic switch logs and maps them to response models.
    /// </summary>
    /// <param name="filterModel">Filtering criteria such as traffic light ID and timestamp range.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A service response containing the list of matching <see cref="TrafficSwitchLogResponseModel"/> records.</returns>
    Task<ServiceResponse<List<TrafficSwitchLogResponseModel>>> GetFilteredTrafficLightSwitchLogs(
        TrafficSwitchLogFilterRequestModel filterModel,
        CancellationToken cancellationToken);
}