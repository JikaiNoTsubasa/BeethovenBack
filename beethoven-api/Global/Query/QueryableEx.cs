using System;
using System.Linq.Expressions;

namespace beethoven_api.Global.Query;

public static class QueryableEx
{
    public static IQueryable<T> Randomize<T>(this IQueryable<T> query, int count){
        return query.OrderBy(x => Guid.NewGuid()).Take(count);
    }

    public static IQueryable<T> Where<T>(
        this IQueryable<T> query,
        bool condition,
        Expression<Func<T, bool>> @where)
    {
        return condition ? query.Where(@where) : query;
    }

    public static IQueryable<T> Search<T>(
        this IQueryable<T> query,
        QuerySearch? search,
        Expression<Func<T, bool>> @where)
    {
        return (search is null || string.IsNullOrEmpty(search.Content) || string.IsNullOrWhiteSpace(search.Content)) ? query : query.Where(@where);
    }

    public static IQueryable<T> Paged<T>(this IQueryable<T> query, QueryPagination? pagination, out QueryMeta? meta){
        if (pagination is null || pagination.Limit <=0){
            meta = new(){
                Total = query.Count(),
            };
            meta.PageSize = meta.Total;
            return query;
        }

        meta = new(){
            Page = pagination.Page,
            Limit = pagination.Limit,
            Total = query.Count()
        };

        query = query.Skip((pagination.Page - 1) * pagination.Limit).Take(pagination.Limit);
        meta.PageSize = query.Count();
        
        return query;
    }
}
