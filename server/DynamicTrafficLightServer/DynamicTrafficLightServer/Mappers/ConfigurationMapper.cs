using DynamicTrafficLightServer.Dtos;
using DynamicTrafficLightServer.Models;
using Riok.Mapperly.Abstractions;

namespace DynamicTrafficLightServer.Mappers;

[Mapper]
public static partial class ConfigurationMapper
{
    [MapperIgnoreSource(nameof(Configuration.CreatedById))]
    [MapperIgnoreSource(nameof(Configuration.LastUpdatedById))]
    [MapperIgnoreSource(nameof(Configuration.TrafficLight))]
    [MapProperty(nameof(Configuration.CreatedBy.Name), nameof(ConfigurationResponseModel.CreatedByName))]
    [MapProperty(nameof(Configuration.LastUpdatedBy.Name), nameof(ConfigurationResponseModel.LastUpdatedByName))]
    public static partial ConfigurationResponseModel ToResponseModel(Configuration configuration);

    [MapperIgnoreTarget(nameof(Configuration.Id))]
    [MapperIgnoreTarget(nameof(Configuration.CreateTime))]
    [MapperIgnoreTarget(nameof(Configuration.CreatedBy))]
    [MapperIgnoreTarget(nameof(Configuration.CreatedById))]
    [MapperIgnoreTarget(nameof(Configuration.LastUpdateTime))]
    [MapperIgnoreTarget(nameof(Configuration.LastUpdatedBy))]
    [MapperIgnoreTarget(nameof(Configuration.LastUpdatedById))]
    [MapperIgnoreTarget(nameof(Configuration.TrafficLight))]
    public static partial Configuration ToModel(ConfigurationRequestModel requestModel);
}