using System.Data;
using System.Data.SqlClient;
using Clever.Collection;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Clever.Repository.AdoNet
{
    public interface IDataBaseClient
    {
        IEnumerable<T> ExectureStoreProcedureList<T>(string spName, object parameters, Func<string, string, bool> matchFunction);
        T ExectureStoreProcedureObject<T>(string spName, object parameters, Func<string, string, bool> matchFunction);
        Tuple<IEnumerable<T1>> ExectureStoreProcedureDataSets<T1>(string spName, object parameters, Func<string, string, bool> matchFunction);
        Tuple<IEnumerable<T1>, IEnumerable<T2>> ExectureStoreProcedureDataSets<T1, T2>(string spName, object parameters, Func<string, string, bool> matchFunction);
        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> ExectureStoreProcedureDataSets<T1, T2, T3>(string spName, object parameters, Func<string, string, bool> matchFunction);
    }

    public abstract class DataBaseClient
    {
        public DataBaseClient(IAdoNetConnection connection)
        {
            Connection = connection.Connection;
        }

        public SqlConnection Connection { get; private set; }

        public SqlDataReader ExectureStoreProcedureDataReader(string spName, IEnumerable<SqlParameter> parameters)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = spName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = Connection;

            parameters.ForEachAndSelect(cmd.Parameters.Add);

            Connection.Open();

            var reader = cmd.ExecuteReader();

            Connection.Close();

            return reader;
        }

        public SqlDataReader ExectureStoreProcedureDataReader(string spName, object parameters, Func<string , string, bool> matchFunction)
        {
            return ExectureStoreProcedureDataReader(spName, SqlParameterUtils.GetParameters(parameters, GetParametersForSp(spName), matchFunction));
        }

        public IEnumerable<T> ExectureStoreProcedureList<T>(string spName, object parameters, Func<string, string, bool> matchFunction)
            where T : new()
        {
            return ExectureStoreProcedureDataReader(spName, parameters, matchFunction).GetDataSet<T>(matchFunction);
        }

        public T ExectureStoreProcedureObject<T>(string spName, object parameters, Func<string, string, bool> matchFunction)
            where T : new()
        {
            return ExectureStoreProcedureList<T>(spName, parameters, matchFunction).FirstOrDefault();
        }

        public Tuple<IEnumerable<T1>> ExectureStoreProcedureDataSets<T1>(string spName, object parameters, Func<string, string, bool> matchFunction)
            where T1 : new()
        {
            return ExectureStoreProcedureDataReader(spName, parameters, matchFunction).GetDataSets<T1>(matchFunction);
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> ExectureStoreProcedureDataSets<T1, T2>(string spName, object parameters, Func<string, string, bool> matchFunction)
            where T1 : new()
            where T2 : new()
        {
            return ExectureStoreProcedureDataReader(spName, parameters, matchFunction).GetDataSets<T1, T2>(matchFunction);
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> ExectureStoreProcedureDataSets<T1, T2, T3>(string spName, object parameters, Func<string, string, bool> matchFunction)
            where T1 : new()
            where T2 : new()
            where T3 : new()
        {
            return ExectureStoreProcedureDataReader(spName, parameters, matchFunction).GetDataSets<T1, T2, T3>(matchFunction);
        }

        public SqlParameter[] GetParametersForSp(string spName)
        {
            return new SqlParameter[0]; // TODO: Get and cache stored procedure paramater schema possibly load all at startup
        }
    }

    public class MappedDataBaseClient
    {
        public MappedDataBaseClient(IDataBaseClient dbClient, Func<string, string, bool> parameterMapper, Func<string, string, bool> resultMapper)
        {
            DbClient = dbClient;
            ParamterMatchFunction = parameterMapper;
        }

        protected readonly Func<string, string, bool> ParamterMatchFunction;

        protected IDataBaseClient DbClient { get; private set; }
        
        public IEnumerable<T> ExectureStoreProcedureList<T>(string spName, object parameters)
        { return DbClient.ExectureStoreProcedureList<T>(spName, parameters, ParamterMatchFunction); }

        public T ExectureStoreProcedureObject<T>(string spName, object parameters, Func<string, string, bool> matchFunction)
        { return DbClient.ExectureStoreProcedureObject<T>(spName, parameters, ParamterMatchFunction); }

        public Tuple<IEnumerable<T1>> ExectureStoreProcedureDataSets<T1>(string spName, object parameters, Func<string, string, bool> matchFunction)
        { return DbClient.ExectureStoreProcedureDataSets<T1>(spName, parameters, ParamterMatchFunction); }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> ExectureStoreProcedureDataSets<T1, T2>(string spName, object parameters, Func<string, string, bool> matchFunction)
        { return DbClient.ExectureStoreProcedureDataSets<T1, T2>(spName, parameters, ParamterMatchFunction); }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> ExectureStoreProcedureDataSets<T1, T2, T3>(string spName, object parameters, Func<string, string, bool> matchFunction)
        { return DbClient.ExectureStoreProcedureDataSets<T1, T2, T3>(spName, parameters, ParamterMatchFunction); }
    }
}
