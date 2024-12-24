using System.ComponentModel.DataAnnotations;

namespace DynamicTrafficLightServer.Models;

public class User
{
    public int Id { get; init; }
    [MaxLength(30)] public required string AuthIdentityId { get; set; }
    public int RoleId { get; set; }

    public Role? Role { get; set; }
}