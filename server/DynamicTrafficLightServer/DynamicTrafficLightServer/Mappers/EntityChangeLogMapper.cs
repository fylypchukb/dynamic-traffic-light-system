using DynamicTrafficLightServer.Dtos.EntityChangeLogDto;
using DynamicTrafficLightServer.Models;
using Riok.Mapperly.Abstractions;

namespace DynamicTrafficLightServer.Mappers;

[Mapper]
public static partial class EntityChangeLogMapper
{
    [MapperIgnoreSource(nameof(EntityChangeLog.ChangedById))]
    public static partial EntityChangeLogResponseModel ToResponseModel(EntityChangeLog log);
}