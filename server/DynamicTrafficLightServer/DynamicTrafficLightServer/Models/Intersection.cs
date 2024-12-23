namespace DynamicTrafficLightServer.Models;

public class Intersection
{
    public int Id { get; set; }
    public required string City { get; set; }
    public required string Location { get; set; }
    public DateTime LastUpdated { get; set; }
    public int LastUpdateById { get; set; }

    public List<TrafficLight>? TrafficLights { get; set; }
    public User? LastUpdatedBy { get; set; }
}