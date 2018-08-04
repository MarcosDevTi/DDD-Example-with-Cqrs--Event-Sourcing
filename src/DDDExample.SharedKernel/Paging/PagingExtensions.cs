using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;

namespace DDDExample.SharedKernel.Paging
{
    public static class PagingExtensions
    {

        private static readonly List<Type> Collections = new List<Type> { typeof(IEnumerable<>), typeof(IEnumerable) };

        private static Paging<TOut> Conversor<TIn, TOut>(Paging<TIn> entrada)
        {
            return new Paging<TOut>
            {
                SortColumn = entrada.SortColumn,
                Top = entrada.Top,
                Skip = entrada.Skip,
                SortDirection = entrada.SortDirection
            };
        }

        public static PagedResult<T2> GetPagedResult<T, T2>(this IQueryable<T> dbSet, Paging<T2> paging)
        {
            var count = dbSet.Count();

            var pagingT2 = Conversor<T2, T>(paging);
            return new PagedResult<T2>(dbSet.SortAndPage2<T, T2>(pagingT2), count, Conversor<T, T2>(pagingT2));
        }

        public static IQueryable<T2> SortAndPage2<T, T2>(this IQueryable<T> dbSet, Paging<T> paging)
        {
            if (paging == null)
            {
                return dbSet.ProjectTo<T2>();
            }

            if (string.IsNullOrEmpty(paging.SortColumn))
            {
                paging.SortColumn = typeof(T)
                    .GetProperties()
                    .First(p => p.PropertyType == typeof(string)
                                || !p.PropertyType.GetInterfaces()
                                    .Any(i => Collections.Any(c => i == c)))
                    .Name;
            }

            var parameter = Expression.Parameter(typeof(T), "p");

            var command = paging.SortDirection == SortDirection.Descending ? "OrderByDescending" : "OrderBy";

            var parts = paging.SortColumn.Split(new [] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            var property = typeof(T).GetProperty(parts[0]);
            var member = Expression.MakeMemberAccess(parameter, property);
            for (var i = 1; i < parts.Length; i++)
            {
                property = property.PropertyType.GetProperty(parts[i]);
                member = Expression.MakeMemberAccess(member, property);
            }

            var orderByExpression = Expression.Lambda(member, parameter);

            var resultExpression = Expression.Call(
                typeof(Queryable),
                command,
                new[] { typeof(T), property.PropertyType },
                dbSet.Expression,
                Expression.Quote(orderByExpression));

            dbSet = dbSet.Provider.CreateQuery<T>(resultExpression);
            return dbSet.Skip(paging.Skip).Take(paging.Top).ProjectTo<T2>();
        }
    }
}
