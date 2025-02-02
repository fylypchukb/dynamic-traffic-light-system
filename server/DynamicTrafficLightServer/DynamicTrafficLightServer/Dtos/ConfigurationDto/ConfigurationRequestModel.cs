namespace DynamicTrafficLightServer.Dtos;

/// <summary>
/// Represents a request model for configuring a traffic light system.
/// </summary>
public record ConfigurationRequestModel
{
    /// <summary>
    /// The unique identifier for the traffic light.
    /// </summary>
    public int TrafficLightId { get; set; }

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
    /// The default duration (in seconds) for the red light when the traffic light operates in autonomous mode 
    /// (disconnected from the server).
    /// </summary>
    public int DefaultRedTime { get; set; }

    /// <summary>
    /// Indicates whether the configuration entity is active.
    /// </summary>
    public bool IsActive { get; set; }
}