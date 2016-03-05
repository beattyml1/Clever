using Clever.Mapping;
using Clever.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clever.Service
{
    public interface IResourceService  <TId, TGetById, TGetMany, TQuery, TPost, TPostReturn, TPut, TPutReturn, TDeleteReturn>
    {
        ServiceOption<TGetById> Get(TId id);
        ServiceOption<IEnumerable<TGetMany>> Get(TQuery query);
        ServiceOption<TPostReturn> Post(TPost data);
        ServiceOption<TPutReturn> Put(TId id, TPut data);
        ServiceOption<TDeleteReturn> Delete(TId id);
    }

    public class ResourceService<
        TId, TGetById, TGetMany, TQuery, TPost, TPostReturn, TPut, TPutReturn, TDeleteReturn, 
        TSession, TGetByIdRepo, TGetManyRepo, TQueryRepo, TPostRepo, TPostReturnRepo, TPutRepo, TPutReturnRepo, TDeleteReturnRepo>

        : IResourceService<TId, TGetById, TGetMany, TQuery, TPost, TPostReturn, TPut, TPutReturn, TDeleteReturn>

        where TGetById: new() 
        where TGetMany : new()
        where TQuery : new()
        where TPost : new()
        where TPostReturn : new()
        where TPut : new()
        where TPutReturn: new()
        where TDeleteReturn: new()
        where TGetByIdRepo : new()
        where TGetManyRepo : new()
        where TQueryRepo : new()
        where TPostRepo : new()
        where TPostReturnRepo : new()
        where TPutRepo : new()
        where TPutReturnRepo : new()
        where TDeleteReturnRepo : new()
    {
        public ResourceService(IContextService<TSession> contextService, IRepository<TId, TGetByIdRepo, TGetManyRepo, TQueryRepo, TPostRepo, TPostReturnRepo, TPutRepo, TPutReturnRepo, TDeleteReturnRepo> repo)
        {
            this.Repository = repo;
            this.Context = contextService;
        }

        public IContextService<TSession> Context { get; private set; }
        public IRepository<TId, TGetByIdRepo, TGetManyRepo, TQueryRepo, TPostRepo, TPostReturnRepo, TPutRepo, TPutReturnRepo, TDeleteReturnRepo> Repository { get; private set; }

        public ServiceOption<TDeleteReturn> Delete(TId id)
        {
            return Repository.Delete(id).ConvertTo<TDeleteReturn>();
        }

        public ServiceOption<IEnumerable<TGetMany>> Get(TQuery query)
        {
            return Repository.Get(query.ConvertTo<TQueryRepo>()).Select(x => x.ConvertTo<TGetMany>()).ToArray();
        }

        public ServiceOption<TGetById> Get(TId id)
        {
            return Repository.Get(id).ConvertTo<TGetById>();
        }

        public ServiceOption<TPostReturn> Post(TPost data)
        {
            return Repository.Post(data.ConvertTo<TPostRepo>()).ConvertTo<TPostReturn>();
        }

        public ServiceOption<TPutReturn> Put(TId id, TPut data)
        {
            return Repository.Put(id, data.ConvertTo<TPutRepo>()).ConvertTo<TPutReturn>();
        }
    }
}
