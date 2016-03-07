using System.Data;
using System.Data.SqlClient;

namespace Clever.Repository.AdoNet
{
    public interface IAdoNetConnection
    {
       SqlConnection Connection { get; }
    }
}