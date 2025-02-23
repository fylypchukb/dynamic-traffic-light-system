﻿using System.Net;
using DynamicTrafficLightServer.Dtos;
using DynamicTrafficLightServer.Repositories.Interfaces;
using DynamicTrafficLightServer.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace DynamicTrafficLightServer.Services.Implementations;

public class TrafficFlowService(IConfigurationRepository configurationRepository, IHubContext<TrafficHub> trafficHub)
    : ITrafficFlowService
{
    /// <inheritdoc />
    public async Task<ServiceResponse<TrafficDataResponse>> CalculateGreenLightAsync(
        TrafficDataRequest trafficDataRequest,
        CancellationToken cancellationToken = default)
    {
        var configuration =
            await configurationRepository.GetByTrafficLightIdAsync(trafficDataRequest.TrafficLightId,
                cancellationToken);

        if (configuration is null)
        {
            return new ServiceResponse<TrafficDataResponse>
            {
                StatusCode = HttpStatusCode.NotFound,
                ErrorMessage = "There is no active configuration for the specified traffic light."
            };
        }

        var greenLightDuration = 0;

        if (configuration.SequenceGreenTime is not null)
        {
            if (configuration.SequenceGreenTime.TryGetValue(trafficDataRequest.CarsNumber, out var duration))
            {
                greenLightDuration = duration;
            }
        }

        if (greenLightDuration == 0)
        {
            greenLightDuration = trafficDataRequest.CarsNumber * configuration.TimePerVehicle;
        }

        if (greenLightDuration > configuration.MaxGreenTime)
        {
            greenLightDuration = configuration.MaxGreenTime;
        }

        if (greenLightDuration < configuration.MinGreenTime)
        {
            greenLightDuration = configuration.MinGreenTime;
        }

        var trafficDataResponse = new TrafficDataResponse
        {
            TrafficLightId = trafficDataRequest.TrafficLightId,
            GreenLightDuration = greenLightDuration
        };

        await trafficHub.Clients.Group(configuration.TrafficLight!.IntersectionId.ToString())
            .SendAsync("ReceiveTrafficLightUpdate", trafficDataResponse, cancellationToken);

        return new ServiceResponse<TrafficDataResponse>
        {
            Result = trafficDataResponse
        };
    }
}