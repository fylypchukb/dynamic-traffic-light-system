namespace DynamicTrafficLightServer.Dtos;

/// <summary>
/// Represents the intersection details returned by the API.
/// </summary>
public record IntersectionResponseModel
{
    /// <summary>
    /// The unique identifier for the intersection.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the city where the intersection is located.
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// The specific location details of the intersection.
    /// </summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// The name of the user who created the intersection record.
    /// </summary>
    public string CreatedByName { get; set; } = string.Empty;

    /// <summary>
    /// The date and time when the intersection record was created.
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// The name of the user who last updated the intersection record.
    /// </summary>
    public string LastUpdatedByName { get; set; } = string.Empty;

    /// <summary>
    /// The date and time when the intersection record was last updated.
    /// </summary>
    public DateTime LastUpdateTime { get; set; }

    /// <summary>
    /// Indicates whether the intersection entity is currently active.
    /// </summary>
    public bool IsActive { get; set; } = true;
}