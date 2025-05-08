namespace DynamicTrafficLightServer.Models;

public class TrafficSwitchLog
{
    public int Id { get; set; }
    public int TrafficLightId { get; set; }
    public int VehicleCount { get; set; }
    public int GreenLightDurationSeconds { get; set; }
    public int InitById { get; set; }
    public DateTime Timestamp { get; set; }

    public User? InitBy { get; set; }
    public TrafficLight? TrafficLight { get; set; }
}