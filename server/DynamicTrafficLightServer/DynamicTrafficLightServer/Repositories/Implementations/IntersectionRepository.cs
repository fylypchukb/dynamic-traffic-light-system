using DynamicTrafficLightServer.Data;
using DynamicTrafficLightServer.Models;
using DynamicTrafficLightServer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DynamicTrafficLightServer.Repositories.Implementations;

public class IntersectionRepository(DataContext context) : IIntersectionRepository
{
    /// <inheritdoc />
    public async Task<List<Intersection>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Intersections.AsNoTracking().ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Intersection?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Intersections.FindAsync([id], cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddAsync(Intersection intersection, CancellationToken cancellationToken = default)
    {
        await context.Intersections.AddAsync(intersection, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Intersection intersection, CancellationToken cancellationToken = default)
    {
        context.Intersections.Update(intersection);
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Intersection intersection, CancellationToken cancellationToken = default)
    {
        context.Intersections.Remove(intersection);
        await context.SaveChangesAsync(cancellationToken);
    }
}