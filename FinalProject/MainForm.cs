using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace FinalProject
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private Customer customer;
        private Account account;
        private SavingsAccount savingsaccount;


        SqlCommand selectCommand;
        SqlDataAdapter dataadapt;
        SqlConnection conn = FinalProjectDB.GetConnection();



        private void GetCustomer(int customerID)
        {
            try
            {
                customer = CustomerDB.GetCustomer(customerID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void ClearControls()
        {
            txtCustomerID.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtState.Text = "";
            btnModify.Enabled = false;
            btnDelete.Enabled = false;
            txtCustomerID.Focus();
        }

        private void DisplayCustomer()
        {
            txtName.Text = customer.Name;
            txtAddress.Text = customer.Address;
            txtPhone.Text = Convert.ToString(customer.Phone);
            txtState.Text = customer.State;
            btnModify.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnGetCustomer_Click(object sender, EventArgs e)
        {
            if (txtCustomerID.Text != "")
            {
                try
                {
                    int customerID = Convert.ToInt32(txtCustomerID.Text);
                    this.GetCustomer(customerID);
                    if (customer == null)
                    {
                        MessageBox.Show("No customer found with this ID. " +
                             "Please try again.", "Customer Not Found");
                        this.ClearControls();
                    }
                    else
                        this.DisplayCustomer();
                }
                catch (FormatException)
                {
                    MessageBox.Show(txtCustomerID.Tag + " must be an integer.", "Entry Error");
                }
            }
            else
            {
                MessageBox.Show(txtCustomerID.Tag + " is a required field.", "Entry Error");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Delete " + customer.Name + "?",
              "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (!CustomerDB.DeleteCustomer(customer))
                    {
                        MessageBox.Show("Another user has updated or deleted " +
                            "that customer.", "Database Error");
                        this.GetCustomer(customer.CustomerID);
                        if (customer != null)
                            this.DisplayCustomer();
                        else
                            this.ClearControls();
                    }
                    else
                        this.ClearControls();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            frmModifyCustomer modifyCustomerForm = new frmModifyCustomer();
            modifyCustomerForm.customer = customer;
            DialogResult result = modifyCustomerForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                customer = modifyCustomerForm.customer;
                this.DisplayCustomer();
            }
            else if (result == DialogResult.Retry)
            {
                this.GetCustomer(customer.CustomerID);
                if (customer != null)
                    this.DisplayCustomer();
                else
                    this.ClearControls();
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddCustomer addCustomerForm = new frmAddCustomer();
            DialogResult result = addCustomerForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                account = addCustomerForm.account;
                customer = addCustomerForm.customer;
                txtCustomerID.Text = customer.CustomerID.ToString();
                this.DisplayCustomer();
            }
        }


        private void btnData_Click(object sender, EventArgs e)
        {
            if (DBUtil.openDbConnection(conn))
            {
                try
                {
                    
                    selectCommand = conn.CreateCommand();

                    selectCommand.CommandText = "SELECT C.Name, C.CustomerID AS 'Customer ID', A.Balance " +
                                                "FROM Customers C " +
                                                "INNER JOIN Accounts A " +
                                                "ON C.AccountID = A.AccountID ";
                    
                    dataadapt = new SqlDataAdapter();
                    
                    dataadapt.SelectCommand = selectCommand;
          
                    DataSet ds = new DataSet();
                    dataadapt.Fill(ds);
                    dataGridView.DataSource = ds.Tables[0];

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SQL Error occurred");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGetName_Click(object sender, EventArgs e)
        {
            if (DBUtil.openDbConnection(conn))
            {
                try
                {

                    selectCommand = conn.CreateCommand();

                    selectCommand.CommandText = "SELECT Name, CustomerID " +
                                                "FROM Customers " +
                                                "WHERE Name LIKE " + "'" + txtCustomerLetter.Text + "%'";
                    

                    dataadapt = new SqlDataAdapter();

                    dataadapt.SelectCommand = selectCommand;

                    DataSet ds = new DataSet();
                    dataadapt.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SQL Error occurred");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAddSavingsAccount_Click(object sender, EventArgs e)
        {
            frmAddSavings addCustomerForm = new frmAddSavings();
            DialogResult result = addCustomerForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                account = addCustomerForm.account;
                customer = addCustomerForm.customer;
                txtCustomerID.Text = customer.CustomerID.ToString();
                this.DisplayCustomer();
            }
        }

        private void GetAccount(int accountID)
        {
            try
            {
                savingsaccount = AccountDB.GetSavingsAccount(accountID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnGetAccount_Click(object sender, EventArgs e)
        {
            if (txtSavingsAccountID.Text != "")
            {
                try
                {
                    int accountID = Convert.ToInt32(txtSavingsAccountID.Text);
                    this.GetAccount(accountID);

                    if (savingsaccount == null)
                    {
                        MessageBox.Show("No Savings Account found with this ID. " +
                             "Please try again.", "Account Not Found");
                        this.ClearControls();
                    }

                    else
                    { 
                        List<Account> AccountList = new List<Account>();
                        AccountList.Add(savingsaccount);
                        AccountList[0].printdata(); 
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show(txtSavingsAccountID.Tag + " must be an integer.", "Entry Error");
                }
            }

            else
            {
                MessageBox.Show(txtSavingsAccountID.Tag + " is a field.", "Entry Error");
            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
                System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Alex-Sem5\FinalProject\sample.txt");
                try
                {
                    string sLine = "";

                    for (int r = 0; r <= dataGridView.Rows.Count - 1; r++)
                    {
                        for (int c = 0; c <= dataGridView.Columns.Count - 1; c++)
                        {
                            sLine = sLine + dataGridView.Rows[r].Cells[c].Value;
                            if (c != dataGridView.Columns.Count - 1)
                            {
                                sLine = sLine + "--";
                            }
                        }
                        
                        file.WriteLine(sLine);
                        sLine = "";
                    }

                    file.Close();
                    MessageBox.Show("Export Complete.", "Program Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    file.Close();
                }
            
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            
            StreamReader file = new StreamReader(@"C:\Alex-Sem5\FinalProject\sample.txt");

            string fileContent = file.ReadToEnd();
            MessageBox.Show(fileContent);

            file.Close();
        }
    }

   
}

