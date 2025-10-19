using Domain_Layer.Models;
using System.Linq.Expressions;

namespace Domain_Layer.Contracts
{
    public interface ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Expression<Func<TEntity, bool>>? Criteria { get; }

        List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }

        Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> OrderByDescending { get; }
    }
}
