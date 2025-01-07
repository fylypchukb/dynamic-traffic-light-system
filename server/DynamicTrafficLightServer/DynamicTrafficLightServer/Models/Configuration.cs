namespace DynamicTrafficLightServer.Models;

public class Configuration
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
    public int CreatedById { get; set; }
    public DateTime LastUpdateTime { get; set; }
    public int LastUpdatedById { get; set; }
    public bool IsActive { get; set; }

    public TrafficLight? TrafficLight { get; set; }
    public User? CreatedBy { get; set; }
    public User? LastUpdatedBy { get; set; }
}