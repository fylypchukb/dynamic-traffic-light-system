namespace DynamicTrafficLightServer.Dtos;

/// <summary>
/// Request model representing a traffic light in the API.
/// </summary>
public record TrafficLightRequestModel
{
    /// <summary>
    /// The name of the traffic light.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The identifier of the intersection the traffic light belongs to.
    /// </summary>
    public int IntersectionId { get; set; }

    /// <summary>
    /// The priority level of the traffic light.
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// Indicates whether the traffic light entity is active.
    /// </summary>
    public bool IsActive { get; set; }
}