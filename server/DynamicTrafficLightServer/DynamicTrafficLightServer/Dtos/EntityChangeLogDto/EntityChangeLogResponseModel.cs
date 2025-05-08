using DynamicTrafficLightServer.Enums;

namespace DynamicTrafficLightServer.Dtos;

/// <summary>
/// Represents a log of changes made to an entity.
/// </summary>
public record EntityChangeLogResponseModel : BaseModelResponseDto
{
    /// <summary>
    /// Name of the changed entity.
    /// </summary>
    public required string EntityName { get; set; }

    /// <summary>
    /// Identifier of the changed entity.
    /// </summary>
    public int EntityId { get; set; }

    /// <summary>
    /// Type of action performed (e.g., Create, Update, Delete).
    /// <seealso cref="EntityChangeAction"/>
    /// </summary>
    public required string Action { get; set; }

    /// <summary>
    /// Name of the user who made the change.
    /// </summary>
    public string ChangedByName { get; set; } = string.Empty;

    /// <summary>
    /// Time the change was logged.
    /// </summary>
    public DateTime Timestamp { get; set; }
}