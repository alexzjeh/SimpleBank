using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;


namespace FinalProject
{
    class AccountDB
    {
        public static int AddAccount(Account account)
        {
            SqlConnection connection = FinalProjectDB.GetConnection();
            string insertStatement =
                "INSERT Accounts " +
                "(DateOpened, Balance, AccountTypeID ) " +
                "VALUES (@DateOpened, @Balance, @AccountTypeID)";
            SqlCommand insertCommand =
                new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue(
                "@DateOpened", account.DateOpened);
            insertCommand.Parameters.AddWithValue(
                "@Balance", account.Balance);
            insertCommand.Parameters.AddWithValue(
                "@AccountTypeID", account.AccountTypeID);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('Accounts') FROM Accounts";
                SqlCommand selectCommand =
                    new SqlCommand(selectStatement, connection);
                int accountID = Convert.ToInt32(selectCommand.ExecuteScalar());
                return accountID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SQL Error occurred");
                return -1;
            }
            finally
            {
                connection.Close();
            }
        }

        public static SavingsAccount GetSavingsAccount(int accountID)
        {
            SqlConnection connection = FinalProjectDB.GetConnection();
            string selectStatement
                = "SELECT AccountID, DateOpened, Balance, AccountTypeID "
                + "FROM Accounts "
                + "WHERE AccountID = @AccountID";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@AccountID", accountID);
            


            try
            {
                connection.Open();
                SqlDataReader custReader =
                    selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                
                if (custReader.Read() && (int)custReader["AccountTypeID"] == 10000)
                {
                    SavingsAccount savingsaccount = new SavingsAccount();
                    savingsaccount.AccountID = (int)custReader["AccountID"];
                    savingsaccount.AccountTypeID = (int)custReader["AccountTypeID"];
                    savingsaccount.DateOpened = Convert.ToDateTime(custReader["DateOpened"]);
                    savingsaccount.Balance = Convert.ToInt32(custReader["Balance"]);
                    return savingsaccount;
                }

                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SQL Error occurred");
                return null;
            }
            

            finally
            {
                connection.Close();
            }



        }
    } }
   

