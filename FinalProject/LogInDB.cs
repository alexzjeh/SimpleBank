using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FinalProject
{
    public delegate void LoginEventHandler(LogIn login, bool status);

    class LogInDB
    {
        public event LoginEventHandler LoginEvent;
        public void Login(string us, string pw)
        {
            SqlConnection connection = FinalProjectDB.GetConnection();
            string selectStatement = "Select * from login where username='" + us + "'AND password='" + pw + "'";
            SqlCommand checkCredentials = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader dataReader = checkCredentials.ExecuteReader();
                dataReader.Read();
                if (dataReader.HasRows)
                {
                    LogIn loggedInUser = new LogIn(dataReader["username"].ToString(), dataReader["password"].ToString());
                    LoginEvent(loggedInUser, true);
                }
                else
                {
                    LogIn nullUser = new LogIn("", "");
                    LoginEvent(nullUser, false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show(e.Message, "Could not connect to database.", MessageBoxButtons.OK);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

