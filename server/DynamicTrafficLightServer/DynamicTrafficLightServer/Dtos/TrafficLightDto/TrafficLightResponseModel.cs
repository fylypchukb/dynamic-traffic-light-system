﻿namespace DynamicTrafficLightServer.Dtos;

/// <summary>
/// Response model representing a traffic light in the API.
/// </summary>
public record TrafficLightResponseModel : BaseModelResponseDto
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
    /// Name of the intersection where the traffic light is located.
    /// </summary>
    public string IntersectionName { get; set; } = string.Empty;

    /// <summary>
    /// The priority level of the traffic light.
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// The timestamp when the traffic light was created.
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// The name of the user who created the traffic light.
    /// </summary>
    public string CreatedByName { get; set; } = string.Empty;

    /// <summary>
    /// The timestamp of the last update to the traffic light.
    /// </summary>
    public DateTime LastUpdateTime { get; set; }

    /// <summary>
    /// The name of the user who last updated the traffic light.
    /// </summary>
    public string LastUpdatedByName { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the traffic light entity is active.
    /// </summary>
    public bool IsActive { get; set; }
}