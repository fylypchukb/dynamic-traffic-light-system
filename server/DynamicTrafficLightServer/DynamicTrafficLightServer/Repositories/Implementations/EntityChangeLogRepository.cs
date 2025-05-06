using DynamicTrafficLightServer.Data;
using DynamicTrafficLightServer.Models;
using DynamicTrafficLightServer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DynamicTrafficLightServer.Repositories.Implementations;

public class EntityChangeLogRepository(DataContext context) : IEntityChangeLogRepository
{
    /// <inheritdoc />
    public async Task<List<EntityChangeLog>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.EntityChangeLogs
            .AsNoTracking()
            .Include(e => e.ChangedBy)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<EntityChangeLog?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.EntityChangeLogs
            .Include(e => e.ChangedBy)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddAsync(EntityChangeLog log, CancellationToken cancellationToken = default)
    {
        await context.EntityChangeLogs.AddAsync(log, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}