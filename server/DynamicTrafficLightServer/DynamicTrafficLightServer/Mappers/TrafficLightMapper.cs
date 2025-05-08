using DynamicTrafficLightServer.Dtos;
using DynamicTrafficLightServer.Models;
using Riok.Mapperly.Abstractions;

namespace DynamicTrafficLightServer.Mappers;

[Mapper]
public static partial class TrafficLightMapper
{
    [MapperIgnoreSource(nameof(TrafficLight.CreatedById))]
    [MapperIgnoreSource(nameof(TrafficLight.LastUpdatedById))]
    [MapperIgnoreSource(nameof(TrafficLight.Intersection))]
    [MapperIgnoreSource(nameof(TrafficLight.Configurations))]
    [MapperIgnoreSource(nameof(TrafficLight.TrafficSwitchLogs))]
    [MapProperty(nameof(TrafficLight.CreatedBy.Name), nameof(TrafficLightResponseModel.CreatedByName))]
    [MapProperty(nameof(TrafficLight.LastUpdatedBy.Name), nameof(TrafficLightResponseModel.LastUpdatedByName))]
    public static partial TrafficLightResponseModel ToResponseModel(TrafficLight trafficLight);

    [MapperIgnoreTarget(nameof(TrafficLight.Id))]
    [MapperIgnoreTarget(nameof(TrafficLight.CreatedById))]
    [MapperIgnoreTarget(nameof(TrafficLight.CreatedBy))]
    [MapperIgnoreTarget(nameof(TrafficLight.LastUpdatedById))]
    [MapperIgnoreTarget(nameof(TrafficLight.LastUpdatedBy))]
    [MapperIgnoreTarget(nameof(TrafficLight.CreateTime))]
    [MapperIgnoreTarget(nameof(TrafficLight.LastUpdateTime))]
    [MapperIgnoreTarget(nameof(TrafficLight.Configurations))]
    [MapperIgnoreTarget(nameof(TrafficLight.Intersection))]
    [MapperIgnoreTarget(nameof(TrafficLight.TrafficSwitchLogs))]
    public static partial TrafficLight ToModel(TrafficLightRequestModel trafficLightRequestModel);
}