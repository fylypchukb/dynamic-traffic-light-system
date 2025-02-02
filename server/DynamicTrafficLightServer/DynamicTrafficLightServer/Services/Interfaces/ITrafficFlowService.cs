using DynamicTrafficLightServer.Dtos;

namespace DynamicTrafficLightServer.Services.Interfaces;

public interface ITrafficFlowService
{
    /// <summary>
    ///     Calculates the green light duration for a traffic light based on the provided traffic data.
    /// </summary>
    /// <param name="trafficDataRequest">The traffic data request containing the traffic light ID and the number of cars.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A service response containing the calculated green light duration.</returns>
    Task<ServiceResponse<TrafficDataResponse>> CalculateGreenLightAsync(TrafficDataRequest trafficDataRequest,
        CancellationToken cancellationToken);
}