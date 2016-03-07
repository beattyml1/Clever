using System.Data;
using Clever.Collection;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Clever.Repository.AdoNet
{
    public static class DataReaderUtilities
    {
        public static IEnumerable<T> GetDataSet<T>(this IDataReader dataReader, Func<string, string, bool> matches) where T : new()
        {
            while (dataReader.Read())
            {
                yield return GetItem<T>(dataReader, matches);
            }
        }

        public static T GetItem<T>(this IDataReader dataReader, Func<string, string, bool> matches) where T : new()
        {
            return SetValues(new T(), dataReader, matches);
        }

        public static T SetValues<T>(T model, IDataReader dataReader, Func<string, string, bool> matches)
        {
            var properties = from drCol in dataReader.GetSchemaTable().Columns.OfType<DataColumn>()
                             from property in typeof(T).GetProperties()
                             where matches(drCol.ColumnName, property.Name)
                             let ordinal = drCol.Ordinal
                             select new { property, value = dataReader.GetValue(ordinal) };

            properties.ForEach(x => x.property.SetValue(model, x.value));

            return model;
        }

        public static Tuple<IEnumerable<T1>> GetDataSets<T1>(this IDataReader dataReader, Func<string, string, bool> matches)
            where T1 : new()
        {
            return new Tuple<IEnumerable<T1>>(
                GetDataSet<T1>(dataReader, matches)
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>> GetDataSets<T1, T2>(this IDataReader dataReader, Func<string, string, bool> matches)
            where T1 : new()
            where T2 : new()
        {
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(
                GetDataSet<T1>(dataReader, matches)
                , GetDataSet<T2>(dataReader, matches)
            );
        }

        public static Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> GetDataSets<T1, T2, T3>(this IDataReader dataReader, Func<string, string, bool> matches)
            where T1 : new()
            where T2 : new()
            where T3 : new()
        {
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(
                GetDataSet<T1>(dataReader, matches)
                , GetDataSet<T2>(dataReader, matches)
                , GetDataSet<T3>(dataReader, matches)
            );
        }
    }
}
