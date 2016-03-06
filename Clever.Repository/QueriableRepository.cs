using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Repository
{
    public abstract class QueryablRepository<TEntity, TId, TQuery>
        : IReadRepository<TId, TEntity, TEntity, TQuery>
        where TEntity : class, new()
    {
        protected readonly IQueryable<TEntity> Queryable;
        public QueryablRepository(IQueryable<TEntity> queryable)
        {
            Queryable = queryable;
        }

        public virtual IEnumerable<TEntity> Get(TQuery query)
        {
            return Queryable.Where(CreateQuery(query));
        }

        public virtual TEntity Get(TId id)
        {
            return Queryable.Single(IdMatches(id));
        }

        protected abstract Expression<Func<TEntity, bool>> CreateQuery(TQuery query);

        protected abstract Expression<Func<TEntity, bool>> IdMatches(TId id);
    }
}
