using Games.Core.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Games.Infrastructure.Repositories.MsSqlServer
{
    internal static class QueryableExtensions
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> entities, Func<bool> condition, Expression<Func<T, bool>> predicate) where T : BaseEntity
        {
            if (condition())
            {
                return entities.Where(predicate);
            }

            return entities;
        }

        public static IQueryable<T> GetPage<T>(this IOrderedQueryable<T> entities, int index, int? size) where T : BaseEntity
        {
            var pageSize = size ?? 10;

            return entities.Skip((index * pageSize) - pageSize).Take(pageSize);
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> entities, string orderByProperty, bool? isDescending) where T : BaseEntity
        {
            var command = isDescending == true ? "OrderByDescending" : "OrderBy";
            var type = typeof(T);
            var property = type.GetProperty(orderByProperty ?? "Id");
            var parameter = Expression.Parameter(type, "p");
            var propertyAcces = Expression.MakeMemberAccess(parameter, property);
            var orderByExpresion = Expression.Lambda(propertyAcces, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new[] { type, property.PropertyType }, entities.Expression, Expression.Quote(orderByExpresion));

            return (IOrderedQueryable<T>)entities.Provider.CreateQuery<T>(resultExpression);
        }
    }
}
