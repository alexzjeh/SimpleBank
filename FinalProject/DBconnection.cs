using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FinalProject
{
    public static class FinalProjectDB
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Data Source=localhost\\TEW_SQLEXPRESS;Initial Catalog=FinalProject; Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
