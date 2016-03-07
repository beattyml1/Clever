using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Clever.Repository.AdoNet
{
    public class SqlParameterUtils
    {
        public static IEnumerable<SqlParameter> GetParameters(object model, SqlParameter[] parameters, Func<string, string, bool> matches)
        {
            return from parameter in parameters
                   from property in model.GetType().GetProperties()
                   where matches(parameter.ParameterName.Replace("@", ""), property.Name)
                   select new SqlParameter(
                       parameter.ParameterName,
                       property.GetValue(model))
                   {
                       Direction = parameter.Direction
                   };
        }
    }
}
