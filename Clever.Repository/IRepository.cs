using System.Collections.Generic;

namespace Clever.Repository
{
    public interface IReadRepository<TId, TGetById, TGetMany, TQuery>
    {
        TGetById Get(TId id);
        IEnumerable<TGetMany> Get(TQuery query);
    }

    public interface IRepository<TId, TGetById, TGetMany, TQuery, TPost, TPostReturn, TPut, TPutReturn, TDeleteReturn>
        : IReadRepository<TId, TGetById, TGetMany, TQuery>
    {
        TPostReturn Post(TPost data);
        TPutReturn Put(TId id, TPut data);
        TDeleteReturn Delete(TId id);
    }
}
