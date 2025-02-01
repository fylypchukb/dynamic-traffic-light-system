namespace DynamicTrafficLightServer.Dtos;

public record ConfigurationRequestModel
{
    public int TrafficLightId { get; set; }
    public int MinGreenTime { get; set; }
    public int MaxGreenTime { get; set; }
    public int TimePerVehicle { get; set; }
    public Dictionary<int, int>? SequenceGreenTime { get; set; }
    public int DefaultGreenTime { get; set; }
    public int DefaultRedTime { get; set; }
    public bool IsActive { get; set; }
}