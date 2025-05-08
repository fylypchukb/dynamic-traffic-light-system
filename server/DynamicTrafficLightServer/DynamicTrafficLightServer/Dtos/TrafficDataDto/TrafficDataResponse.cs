namespace DynamicTrafficLightServer.Dtos;

public record TrafficDataResponse
{
    public int TrafficLightId { get; set; }
    public int GreenLightDuration { get; set; }
    public Guid CorrelationId { get; set; }

    public DateTime DetectionTime { get; set; }
}