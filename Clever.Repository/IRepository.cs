namespace Clever.Repository
{
    public interface IRepository<TId, TGetById, TGetMany, TQuery, TPost, TPostReturn, TPut, TPutReturn, TDeleteReturn>
    {
        TGetById Get(TId id);
        TGetMany[] Get(TQuery query);
        TPostReturn Post(TPost data);
        TPutReturn Put(TId id, TPut data);
        TDeleteReturn Delete(TId id);
    }
}
