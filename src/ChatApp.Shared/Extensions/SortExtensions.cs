using ChatApp.Common.Constants;
using ChatApp.Common.Models;
using System.Linq.Expressions;

namespace ChatApp.Common.Extensions;

public static class SortExtensions
{
    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IEnumerable<SortModel> sortModels)
    {
        var expression = source.Expression;
        int count = 0;
        foreach (var item in sortModels)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var selector = Expression.PropertyOrField(parameter, item.Field ?? OrderConstants.Fields.CreatedDate);
            var method = GetMethod(item.Sort, count);
            expression = Expression.Call(typeof(Queryable), method,
                [source.ElementType, selector.Type],
                expression, Expression.Quote(Expression.Lambda(selector, parameter)));
            count++;
        }
        return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
    }

    private static string GetMethod(string? sort, int count)
    {
        if (string.Equals(OrderConstants.Desc, sort, StringComparison.OrdinalIgnoreCase))
            return count == 0 ? OrderConstants.OrderByDescending : OrderConstants.ThenByDescending;

        return count == 0 ? OrderConstants.OrderBy : OrderConstants.ThenBy;
    }
}
