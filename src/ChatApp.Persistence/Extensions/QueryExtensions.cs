using ChatApp.Common.Models;
using ChatApp.Domain.Entities;
using ChatApp.Common.Extensions;

namespace ChatApp.Persistence.Extensions;

public static class QueryExtensions
{
    public static IQueryable<T> BuildOrderQuery<T>(this IQueryable<T> source, SortModel sortModel)
        where T : BaseEntity
    {
        if (string.IsNullOrWhiteSpace(sortModel.Field))
        {
            return source.OrderByDescending(n => n.CreatedAt);
        }
        else
        {
            var sortModels = new List<SortModel>
            {
                sortModel
            };

            return source.OrderBy(sortModels);
        }
    }
}
