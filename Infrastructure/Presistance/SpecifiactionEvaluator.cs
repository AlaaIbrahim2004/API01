using Domain_Layer.Contracts;
using Domain_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace Presistance
{
    static class SpecifiactionEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> InputQuery, ISpecification<TEntity, TKey> specification) where TEntity : BaseEntity<TKey>
        {
            var query = InputQuery;
            if (specification.Criteria is not null)
            {
                query = query.Where(specification.Criteria);

            }

            if (specification.OrderBy is not null)
            {
                query = query.OrderBy(specification.OrderBy);
            }

            if (specification.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.IncludeExpressions is not null && specification.IncludeExpressions.Any())
            {

                //foreach (var expression in specification.IncludeExpressions)
                //{
                //    query = query.Include(expression);
                //}

                query = specification.IncludeExpressions.Aggregate(query, (curentQuery, includeExpr) => curentQuery.Include(includeExpr));
            }
            return query;
        }
    }
}
