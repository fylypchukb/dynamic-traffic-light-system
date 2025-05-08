using DynamicTrafficLightServer.Dtos;
using DynamicTrafficLightServer.Models;
using Riok.Mapperly.Abstractions;

namespace DynamicTrafficLightServer.Mappers;

[Mapper]
public static partial class TrafficSwitchLogMapper
{
    [MapperIgnoreSource(nameof(TrafficSwitchLog.InitById))]
    [MapperIgnoreSource(nameof(TrafficSwitchLog.TrafficLight))]
    public static partial TrafficSwitchLogResponseModel ToResponseModel(TrafficSwitchLog log);
}