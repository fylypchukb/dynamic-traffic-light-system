namespace DynamicTrafficLightServer.Dtos;

/// <summary>
/// Represents a response model for traffic light configuration details.
/// </summary>
public record ConfigurationResponseModel : BaseModelResponseDto
{
    /// <summary>
    /// The unique identifier for the traffic light associated with this configuration.
    /// </summary>
    public int TrafficLightId { get; set; }

    /// <summary>
    /// The traffic light's name
    /// </summary>
    public string TrafficLightName { get; set; } = string.Empty;

    /// <summary>
    /// The minimum duration (in seconds) that the green light should stay on.
    /// </summary>
    public int MinGreenTime { get; set; }

    /// <summary>
    /// The maximum duration (in seconds) that the green light can stay on.
    /// </summary>
    public int MaxGreenTime { get; set; }

    /// <summary>
    /// The time (in seconds) allocated per detected vehicle for adjusting the green light duration.
    /// </summary>
    public int TimePerVehicle { get; set; }

    /// <summary>
    /// A mapping where the key represents the number of cars detected, and the value represents the duration 
    /// (in seconds) for the green light to stay on for that number of cars.
    /// </summary>
    public Dictionary<int, int>? SequenceGreenTime { get; set; }

    /// <summary>
    /// The default duration (in seconds) for the green light when the traffic light operates in autonomous mode 
    /// (disconnected from the server).
    /// </summary>
    public int DefaultGreenTime { get; set; }

    /// <summary>
    /// he default duration (in seconds) for the red light when the traffic light operates in autonomous mode 
    /// (disconnected from the server).
    /// </summary>
    public int DefaultRedTime { get; set; }

    /// <summary>
    /// The date and time when this configuration was created.
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// The name of the user who created this configuration.
    /// </summary>
    public string CreatedByName { get; set; } = string.Empty;

    /// <summary>
    /// The date and time when this configuration was last updated.
    /// </summary>
    public DateTime LastUpdateTime { get; set; }

    /// <summary>
    /// The name of the user who last updated this configuration.
    /// </summary>
    public string LastUpdatedByName { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the configuration is currently active.
    /// </summary>
    public bool IsActive { get; set; }
}