namespace DynamicTrafficLightServer.Dtos;

public record TrafficDataRequest
{
    public int TrafficLightId { get; set; }
    public int CarsNumber { get; set; }
    public Guid CorrelationId { get; set; }

    public DateTime DetectionTime { get; set; }
}