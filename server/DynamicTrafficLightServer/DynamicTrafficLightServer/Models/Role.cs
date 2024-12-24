using System.ComponentModel.DataAnnotations;

namespace DynamicTrafficLightServer.Models;

public class Role
{
    public int Id { get; set; }
    [MaxLength(50)] public required string Name { get; set; }

    public List<User>? Users { get; set; }
}