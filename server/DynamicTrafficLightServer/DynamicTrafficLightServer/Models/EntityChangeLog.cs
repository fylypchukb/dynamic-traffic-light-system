using System.ComponentModel.DataAnnotations;
using DynamicTrafficLightServer.Enums;

namespace DynamicTrafficLightServer.Models;

public class EntityChangeLog
{
    public int Id { get; set; }
    [MaxLength(50)] public string EntityName { get; set; } = string.Empty;
    public int EntityId { get; set; }
    public EntityChangeAction Action { get; set; }
    public int ChangedById { get; set; }
    public DateTime Timestamp { get; set; }

    public User? ChangedBy { get; set; }
}