using DynamicTrafficLightServer.Enums;

namespace DynamicTrafficLightServer.Dtos.EntityChangeLogDto;

/// <summary>
/// Represents filter parameters for querying entity change logs.
/// </summary>
public record EntityChangeLogFilterRequestModel
{
    /// <summary>
    /// The name of the entity to filter by.
    /// </summary>
    public string? EntityName { get; set; }

    /// <summary>
    /// The ID of the entity to filter by.
    /// </summary>
    public int? EntityId { get; set; }

    /// <summary>
    /// The type of action performed (e.g., Created, Updated, Deleted).
    /// </summary>
    /// <seealso cref="EntityChangeAction"/>
    public EntityChangeAction? Action { get; set; }

    /// <summary>
    /// Start of the timestamp range for filtering.
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End of the timestamp range for filtering.
    /// </summary>
    public DateTime? To { get; set; }
}