using DynamicTrafficLightServer.Dtos;
using DynamicTrafficLightServer.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace DynamicTrafficLightServer;

public class TrafficHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var intersectionId = httpContext?.Request.Query["intersectionId"].FirstOrDefault();

        if (!string.IsNullOrEmpty(intersectionId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, intersectionId);
            Console.WriteLine($"Client {Context.ConnectionId} added to group {intersectionId}");
        }
        else
        {
            Console.WriteLine($"Client {Context.ConnectionId} connected without an intersection parameter.");
        }

        await base.OnConnectedAsync();
    }

    public Task SendTrafficFlowUpdate(TrafficDataRequest trafficDataRequest, ITrafficFlowService trafficFlowService) =>
        trafficFlowService.CalculateGreenLightAsync(trafficDataRequest, Context.ConnectionAborted);
}