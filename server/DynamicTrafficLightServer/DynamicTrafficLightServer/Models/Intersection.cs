using System.ComponentModel.DataAnnotations;

namespace DynamicTrafficLightServer.Models;

public class Intersection
{
    public int Id { get; set; }
    [MaxLength(50)] public required string City { get; set; }
    [MaxLength(100)] public required string Location { get; set; }
    public DateTime LastUpdated { get; set; }
    public int LastUpdateById { get; set; }

    public List<TrafficLight>? TrafficLights { get; set; }
    public User? LastUpdatedBy { get; set; }
}