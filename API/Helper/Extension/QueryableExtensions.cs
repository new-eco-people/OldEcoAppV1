using System.Linq;
using API.Core.Domain.Application.Request.Generic;

namespace API.Helper.Extension
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> AddPagination<T>(this IQueryable<T> query, QueryResult filter) {
            return query.Skip((filter.Page.Value - 1) * filter.PageSize.Value).Take(filter.PageSize.Value);
            // return query;
        }
    }
}