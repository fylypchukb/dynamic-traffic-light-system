using System.ComponentModel.DataAnnotations;

namespace DynamicTrafficLightServer.Models;

public class TrafficLight
{
    public int Id { get; set; }
    public int IntersectionId { get; set; }
    [MaxLength(50)] public string Name { get; set; } = string.Empty;
    public int Priority { get; set; }
    public DateTime CreateTime { get; set; }
    public int CreatedById { get; set; }
    public DateTime LastUpdateTime { get; set; }
    public int LastUpdateById { get; set; }
    public bool IsActive { get; set; }

    public Intersection? Intersection { get; set; }
    public List<Configuration>? Configurations { get; set; }
    public User? CreatedBy { get; set; }
    public User? LastUpdatedBy { get; set; }
}