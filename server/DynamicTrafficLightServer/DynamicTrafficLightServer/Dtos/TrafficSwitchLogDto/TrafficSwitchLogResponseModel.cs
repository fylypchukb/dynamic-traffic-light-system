namespace DynamicTrafficLightServer.Dtos.TrafficSwitchLogDto;

/// <summary>
/// Response model for a traffic switch log.
/// </summary>
public record TrafficSwitchLogResponseModel : BaseModelResponseDto
{
    /// <summary>
    /// ID of the traffic light.
    /// </summary>
    public int TrafficLightId { get; set; }

    /// <summary>
    /// The number of vehicles detected.
    /// </summary>
    public int VehicleCount { get; set; }

    /// <summary>
    /// The green light duration in seconds.
    /// </summary>
    public int GreenLightDurationSeconds { get; set; }

    /// <summary>
    /// The name of the user who initiated the switch.
    /// </summary>
    public string InitByName { get; set; } = string.Empty;

    /// <summary>
    /// The timestamp of the switch init entry.
    /// </summary>
    public DateTime Timestamp { get; set; }
}