namespace DynamicTrafficLightServer.Dtos;

public record ConfigurationResponseModel
{
    public int Id { get; set; }
    public int TrafficLightId { get; set; }
    public int MinGreenTime { get; set; }
    public int MaxGreenTime { get; set; }
    public int TimePerVehicle { get; set; }
    public Dictionary<int, int>? SequenceGreenTime { get; set; }
    public int DefaultGreenTime { get; set; }
    public int DefaultRedTime { get; set; }
    public DateTime CreateTime { get; set; }
    public string CreatedByName { get; set; } = string.Empty;
    public DateTime LastUpdateTime { get; set; }
    public string LastUpdatedByName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}