namespace DynamicTrafficLightServer.Models;

public class TrafficLight
{
    public int Id { get; set; }
    public int IntersectionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Priority { get; set; }
    public DateTime LastUpdated { get; set; }
    public int LastUpdateById { get; set; }

    public Intersection? Intersection { get; set; }
    public User? LastUpdatedBy { get; set; }
}