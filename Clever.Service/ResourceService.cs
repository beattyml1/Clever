using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Service
{
    public interface IResourceService  <TId, TGetById, TGetMany, TQuery, TPost, TPostReturn, TPut, TPutReturn, TDeleteReturn, TSession>
    {
        ServiceOption<TGetById> Get(TId id, TSession session = default(TSession));
        ServiceOption<IEnumerable<TGetMany>> Get(TQuery query, TSession session = default(TSession));
        ServiceOption<TPostReturn> Post(TPost data, TSession session = default(TSession));
        ServiceOption<TPutReturn> Put(TId id, TPut data, TSession session = default(TSession));
        ServiceOption<TDeleteReturn> Delete(TId id, TSession session = default(TSession));
    }

    public class ResourceService<TId, TGetById, TGetMany, TQuery, TPost, TPostReturn, TPut, TPutReturn, TDeleteReturn, TSession>
    {
        public ResourceService()
        {

        }
    }
}
