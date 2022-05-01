using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FinalProject
{
    class DBUtil
    {
        public static bool openDbConnection(SqlConnection conn)
        {
            try
            {
                //Open the connection
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
