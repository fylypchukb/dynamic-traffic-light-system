using DynamicTrafficLightServer.Data;
using DynamicTrafficLightServer.Dtos.EntityChangeLogDto;
using DynamicTrafficLightServer.Extensions;
using DynamicTrafficLightServer.Models;
using DynamicTrafficLightServer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DynamicTrafficLightServer.Repositories.Implementations;

public class EntityChangeLogRepository(DataContext context) : IEntityChangeLogRepository
{
    /// <inheritdoc />
    public async Task<List<EntityChangeLog>> GetFilteredAsync(EntityChangeLogFilterRequestModel filter,
        CancellationToken cancellationToken)
    {
        var query = context.EntityChangeLogs
            .AsNoTracking()
            .Include(e => e.ChangedBy)
            .AsQueryable();

        query
            .WhereIf(!string.IsNullOrWhiteSpace(filter.EntityName), e => e.EntityName == filter.EntityName)
            .WhereIf(filter.EntityId.HasValue, e => e.EntityId == filter.EntityId)
            .WhereIf(filter.Action.HasValue, e => e.Action == filter.Action)
            .WhereIf(filter.From.HasValue, e => e.Timestamp >= filter.From)
            .WhereIf(filter.To.HasValue, e => e.Timestamp <= filter.To);

        return await query.ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<EntityChangeLog?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.EntityChangeLogs
            .Include(e => e.ChangedBy)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddAsync(EntityChangeLog log, CancellationToken cancellationToken)
    {
        await context.EntityChangeLogs.AddAsync(log, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}