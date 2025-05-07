using System.Linq.Expressions;

namespace DynamicTrafficLightServer.Extensions;

public static class QueryableExtensions
{
    /// <summary>
    /// Conditionally applies a <c>Where</c> clause to the query if the specified condition is true.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the queryable sequence.</typeparam>
    /// <param name="source">The queryable data source.</param>
    /// <param name="condition">The condition that determines whether the filter should be applied.</param>
    /// <param name="predicate">The filter expression to apply if the condition is true.</param>
    /// <returns>The original query if the condition is false; otherwise, the filtered query.</returns>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition,
        Expression<Func<T, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }
}