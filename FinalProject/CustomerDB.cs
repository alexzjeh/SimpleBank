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
    public static class CustomerDB
    {
        public static Customer GetCustomer(int customerID)
        {
            SqlConnection connection = FinalProjectDB.GetConnection();
            string selectStatement
                = "SELECT CustomerID, Name, Phone, Address, State "
                + "FROM Customers "
                + "WHERE CustomerID = @CustomerID";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@CustomerID", customerID);

            try
            {
                connection.Open();
                SqlDataReader custReader =
                    selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (custReader.Read())
                {
                    Customer customer = new Customer();
                    customer.CustomerID = (int)custReader["CustomerID"];
                    customer.Name = custReader["Name"].ToString();
                    customer.Address = custReader["Address"].ToString();
                    customer.Phone = (int)custReader["Phone"];
                    customer.State = custReader["State"].ToString();
                    return customer;
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

        public static int AddCustomer(Customer customer)
        {
            SqlConnection connection = FinalProjectDB.GetConnection();
            string insertStatement =
                "INSERT Customers " +
                "(Name, Phone, State, Address, AccountID) " +
                "VALUES (@Name, @Phone, @State, @Address, @AccountID)";
            SqlCommand insertCommand =
                new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue(
                "@Name", customer.Name);
            insertCommand.Parameters.AddWithValue(
                "@Phone", customer.Phone);
            insertCommand.Parameters.AddWithValue(
                "@Address", customer.Address);
            insertCommand.Parameters.AddWithValue(
                "@State", customer.State);
            insertCommand.Parameters.AddWithValue(
                "@AccountID", customer.AccountID);
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement =
                    "SELECT IDENT_CURRENT('Customers') FROM Customers";
                SqlCommand selectCommand =
                    new SqlCommand(selectStatement, connection);
                int customerID = Convert.ToInt32(selectCommand.ExecuteScalar());
                return customerID;
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
            public static bool UpdateCustomer(Customer oldCustomer,
            Customer newCustomer)
            {
                SqlConnection connection = FinalProjectDB.GetConnection();
            string updateStatement =
                "UPDATE Customers SET " +
                "Name = @NewName, " +
                "Phone = @NewPhone, " +
                "Address = @NewAddress, " +
                "State = @NewState " +
                "WHERE CustomerID = @oldCustomerID ";
                    
                SqlCommand updateCommand =
                    new SqlCommand(updateStatement, connection);
                updateCommand.Parameters.AddWithValue(
                    "@NewName", newCustomer.Name);
                updateCommand.Parameters.AddWithValue(
                    "@NewAddress", newCustomer.Address);
                updateCommand.Parameters.AddWithValue(
                    "@NewPhone", newCustomer.Phone);
                updateCommand.Parameters.AddWithValue(
                    "@NewState", newCustomer.State);
                updateCommand.Parameters.AddWithValue(
                    "@OldCustomerID", oldCustomer.CustomerID);
       
                try
                {
                    connection.Open();
                    int count = updateCommand.ExecuteNonQuery();
                    if (count > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SQL Error occurred");
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }

        public static bool DeleteCustomer(Customer customer)
        {
            SqlConnection connection = FinalProjectDB.GetConnection();
            string deleteStatement =
                "DELETE FROM Accounts " +
                "WHERE AccountID = (@CustomerID + 1000)" + 
                "DELETE FROM Customers " +
                "WHERE CustomerID = @CustomerID";

            SqlCommand deleteCommand =
                new SqlCommand(deleteStatement, connection);
            deleteCommand.Parameters.AddWithValue(
                "@CustomerID", customer.CustomerID);
            try
            {
                connection.Open();
                int count = deleteCommand.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SQL Error occurred");
                return false;
            }
            finally
            {
                connection.Close();
            }

        }





     }
}