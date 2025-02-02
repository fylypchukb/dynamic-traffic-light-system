namespace DynamicTrafficLightServer.Dtos;

public record TrafficDataRequest
{
    public int TrafficLightId { get; set; }
    public int CarsNumber { get; set; }
}