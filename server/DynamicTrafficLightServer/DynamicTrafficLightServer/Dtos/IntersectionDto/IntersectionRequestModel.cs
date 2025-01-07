namespace DynamicTrafficLightServer.Dtos;

/// <summary>
/// Represents the data required to create or update an intersection.
/// </summary>
public record IntersectionRequestModel
{
    /// <summary>
    /// The name of the city where the intersection is located.
    /// </summary>
    public required string City { get; set; }

    /// <summary>
    /// The specific location details of the intersection.
    /// </summary>
    public required string Location { get; set; }

    /// <summary>
    /// Indicates whether the intersection is active.
    /// </summary>
    public bool IsActive { get; set; }
}