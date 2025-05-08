using DynamicTrafficLightServer.Data;
using DynamicTrafficLightServer.Models;
using DynamicTrafficLightServer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DynamicTrafficLightServer.Repositories.Implementations;

public class IntersectionRepository(DataContext context) : IIntersectionRepository
{
    /// <inheritdoc />
    public async Task<List<Intersection>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Intersections
            .AsNoTracking()
            .Include(i => i.CreatedBy)
            .Include(i => i.LastUpdatedBy)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Intersection?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Intersections
            .Include(i => i.CreatedBy)
            .Include(i => i.LastUpdatedBy)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddAsync(Intersection intersection, CancellationToken cancellationToken)
    {
        await context.Intersections.AddAsync(intersection, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        await context.Entry(intersection)
            .Reference(i => i.CreatedBy)
            .LoadAsync(cancellationToken);

        await context.Entry(intersection)
            .Reference(i => i.LastUpdatedBy)
            .LoadAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Intersection intersection, CancellationToken cancellationToken)
    {
        context.Intersections.Update(intersection);
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Intersection intersection, CancellationToken cancellationToken)
    {
        context.Intersections.Remove(intersection);
        await context.SaveChangesAsync(cancellationToken);
    }
}