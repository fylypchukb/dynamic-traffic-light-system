using System.ComponentModel.DataAnnotations;

namespace DynamicTrafficLightServer.Models;

public class User
{
    public int Id { get; init; }
    [MaxLength(50)] public required string Name { get; set; }
    [MaxLength(30)] public required string AuthIdentityId { get; set; }
    public int RoleId { get; set; }

    public List<Intersection>? CreatedIntersections { get; set; }
    public List<Intersection>? LastUpdatedIntersections { get; set; }
    public List<TrafficLight>? CreatedTrafficLights { get; set; }
    public List<TrafficLight>? LastUpdatedTrafficLights { get; set; }
    public List<Configuration>? CreatedConfigurations { get; set; }
    public List<Configuration>? LastUpdateConfigurations { get; set; }
    public Role? Role { get; set; }
}