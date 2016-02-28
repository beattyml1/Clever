using Clever.Repository;
using System;
using System.Collections.Generic;

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
        : IResourceService<TId, TGetById, TGetMany, TQuery, TPost, TPostReturn, TPut, TPutReturn, TDeleteReturn, TSession>
    {
        public ResourceService()
        {

        }

        public ServiceOption<TDeleteReturn> Delete(TId id, TSession session = default(TSession))
        {
            throw new NotImplementedException();
        }

        public ServiceOption<IEnumerable<TGetMany>> Get(TQuery query, TSession session = default(TSession))
        {
            throw new NotImplementedException();
        }

        public ServiceOption<TGetById> Get(TId id, TSession session = default(TSession))
        {
            throw new NotImplementedException();
        }

        public ServiceOption<TPostReturn> Post(TPost data, TSession session = default(TSession))
        {
            throw new NotImplementedException();
        }

        public ServiceOption<TPutReturn> Put(TId id, TPut data, TSession session = default(TSession))
        {
            throw new NotImplementedException();
        }
    }
}
