using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Ruifei.Common
{
    public static class SqlHelper
    {
        public static async Task<DataTable> ExecuteDataTableAsync(this SqlConnection cnn, CommandDefinition command)
        {
            var ds = new DataSet();
            var reader = await cnn.ExecuteReaderAsync(command);
            ds.Load(reader, LoadOption.OverwriteChanges, "table1");
            return ds.Tables[0];
        }

        public static DataTable ExecuteDataTable(this SqlConnection cnn, string command)
        {
            var ds = new DataSet();
            SqlDataAdapter Adapter = new SqlDataAdapter(command, cnn);
            DataTable dataTable = new DataTable();
            Adapter.Fill(dataTable);
            return dataTable;
        }
    }
}
