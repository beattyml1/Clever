using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Repository
{
    public interface IRepository<TId, TGetById, TGetMany, TQuery, TPost, TPostReturn, TPut, TPutReturn, TDeleteReturn, TSession>
    {
        TGetById Get(TId id, TSession session = default(TSession));
        TGetMany[] Get(TQuery query, TSession session = default(TSession));
        TPostReturn Post(TPost data, TSession session = default(TSession));
        TPutReturn Put(TId id, TPut data, TSession session = default(TSession));
        TDeleteReturn Delete(TId id, TSession session = default(TSession));
    }
}
