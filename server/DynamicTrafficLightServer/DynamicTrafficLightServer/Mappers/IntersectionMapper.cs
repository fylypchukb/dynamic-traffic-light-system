using DynamicTrafficLightServer.Dtos;
using DynamicTrafficLightServer.Models;
using Riok.Mapperly.Abstractions;

namespace DynamicTrafficLightServer.Mappers;

[Mapper]
public static partial class IntersectionMapper
{
    [MapperIgnoreSource(nameof(Intersection.CreatedById))]
    [MapperIgnoreSource(nameof(Intersection.LastUpdatedById))]
    [MapperIgnoreSource(nameof(Intersection.TrafficLights))]
    [MapProperty(nameof(Intersection.CreatedBy.Name), nameof(IntersectionResponseModel.CreatedByName))]
    [MapProperty(nameof(Intersection.LastUpdatedBy.Name), nameof(IntersectionResponseModel.LastUpdatedByName))]
    public static partial IntersectionResponseModel ToResponseModel(Intersection intersection);
    
    [MapperIgnoreTarget(nameof(Intersection.Id))]
    [MapperIgnoreTarget(nameof(Intersection.CreatedById))]
    [MapperIgnoreTarget(nameof(Intersection.CreatedBy))]
    [MapperIgnoreTarget(nameof(Intersection.LastUpdatedById))]
    [MapperIgnoreTarget(nameof(Intersection.LastUpdatedBy))]
    [MapperIgnoreTarget(nameof(Intersection.CreateTime))]
    [MapperIgnoreTarget(nameof(Intersection.LastUpdateTime))]
    [MapperIgnoreTarget(nameof(Intersection.TrafficLights))]
    public static partial Intersection ToModel(IntersectionRequestModel requestModel);
}