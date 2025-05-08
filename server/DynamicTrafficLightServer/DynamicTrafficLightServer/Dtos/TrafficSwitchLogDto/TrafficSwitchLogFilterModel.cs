namespace DynamicTrafficLightServer.Dtos;

/// <summary>
/// Represents filter parameters for querying traffic switch logs.
/// </summary>
public record TrafficSwitchLogFilterModel
{
    /// <summary>
    /// The ID of the traffic light to filter by.
    /// </summary>
    public int? TrafficLightId { get; set; }

    /// <summary>
    /// Start of the timestamp range for filtering.
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End of the timestamp range for filtering.
    /// </summary>
    public DateTime? To { get; set; }
}