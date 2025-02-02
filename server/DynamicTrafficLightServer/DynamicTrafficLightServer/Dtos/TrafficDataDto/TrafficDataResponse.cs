namespace DynamicTrafficLightServer.Dtos;

public record TrafficDataResponse
{
    public int TrafficLightId { get; set; }
    public int GreenLightDuration { get; set; }
}