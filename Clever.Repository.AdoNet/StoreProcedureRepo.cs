using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Repository.AdoNet
{
    public abstract class StoreProcedureRepoBase
    {
        public StoreProcedureRepoBase(IDataBaseClient dbClient, Func<string, string, bool> mapper) : this(dbClient, mapper, mapper) { }
        public StoreProcedureRepoBase(IDataBaseClient dbClient, Func<string, string, bool> parameterMapper, Func<string, string, bool> resultMapper)
        {
            UnmappedDbClient = dbClient;
            MappedDbClient = new MappedDataBaseClient(dbClient, parameterMapper, resultMapper);
        }

        protected IDataBaseClient UnmappedDbClient { get; private set; }
        protected MappedDataBaseClient MappedDbClient { get; private set; }

    }

    //public interface ISpNamingStandard
    //{
    //    string GetOne(string resouceName);
    //    string GetMany(string resouceName);
    //    string Put(string resouceName);
    //    string Post(string resouceName);
    //    string Delete(string resouceName);
    //}

    //public abstract class AutoStoreProcedureRepo<TID, TGetById, TGetMany, TQuery, TPost, TPut, TPostReturn, TPutReturn, TDeleteReturn>
    //    : StoreProcedureRepoBase
    //    , IRepository<TID, TGetById, TGetMany, TQuery, TPost, TPostReturn, TPut, TPutReturn, TDeleteReturn>
    //{
    //    public AutoStoreProcedureRepo(IDataBaseClient dbClient, string resourceName, ISpNamingStandard spNamingStatdard, bool postReturns, bool putReturns, bool deleteReturnsResults, Func<string, string, bool> parameterMapper, Func<string, string, bool> resultMapper) : base(dbClient, parameterMapper, resultMapper) { }
        
    //    public TPostReturn Post(TPost data)
    //    {
    //        return MappedDbClient
    //    }

    //    public TPutReturn Put(TID id, TPut data)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public TDeleteReturn Delete(TID id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public TGetById Get(TID id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IEnumerable<TGetMany> Get(TQuery query)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
