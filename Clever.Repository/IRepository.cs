using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Repository
{
    public interface IRepository<TId, TGetById, TGetMany, TQuery, TPost, TPostReturn, TPut, TPutReturn, TDeleteReturn, TSession>
    {
        TGetById Get(TId id);
        TGetMany[] Get(TQuery query);
        TPostReturn Post(TPost data);
        TPutReturn Put(TId id, TPut data);
        TDeleteReturn Delete(TId id);
    }
}
