using DynamicTrafficLightServer.Data;
using DynamicTrafficLightServer.Dtos;
using DynamicTrafficLightServer.Extensions;
using DynamicTrafficLightServer.Models;
using DynamicTrafficLightServer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DynamicTrafficLightServer.Repositories.Implementations;

public class TrafficSwitchLogRepository(DataContext context) : ITrafficSwitchLogRepository
{
    /// <inheritdoc />
    public async Task<List<TrafficSwitchLog>> GetFilteredAsync(TrafficSwitchLogFilterModel filter,
        CancellationToken cancellationToken)
    {
        var query = context.TrafficSwitchLogs
            .AsNoTracking()
            .Include(x => x.InitBy)
            .Include(x => x.TrafficLight)
            .AsQueryable();

        query = query
            .WhereIf(filter.TrafficLightId.HasValue, x => x.TrafficLightId == filter.TrafficLightId)
            .WhereIf(filter.From.HasValue, x => x.Timestamp >= filter.From)
            .WhereIf(filter.To.HasValue, x => x.Timestamp <= filter.To);

        return await query.ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TrafficSwitchLog?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.TrafficSwitchLogs
            .Include(l => l.InitBy)
            .Include(l => l.TrafficLight)
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddAsync(TrafficSwitchLog log, CancellationToken cancellationToken)
    {
        await context.TrafficSwitchLogs.AddAsync(log, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}