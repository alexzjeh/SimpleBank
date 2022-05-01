using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FinalProject
{
    public partial class frmAddCustomer : Form
    {
        public frmAddCustomer()
        {
            InitializeComponent();
        }

        public Customer customer;
        public Account account;
        private int z;
        private int l;

        private void frmAddCustomer_Load(object sender, EventArgs e)
        {
                this.LoadStateComboBox();
                this.Text = "Add Customer";
                cboStates.SelectedIndex = -1;
        }
        
        private void LoadStateComboBox()
        {
            /*List<State> states = new List<State>();
            try
            {
                states = StateDB.GetStates();
                cboStates.DataSource = states;
                cboStates.DisplayMember = "StateName";
                cboStates.ValueMember = "StateCode";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
            */
            Dictionary<string, string> states = new Dictionary<string, string>();
            try
            {
                states = StateDB.GetStatesDictionary();
                cboStates.DataSource = new BindingSource(states, null);
                cboStates.DisplayMember = "Value";
                cboStates.ValueMember = "Key";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

        }
    

        private void btnAccept_Click(object sender, EventArgs e)
        {
               
                account = new Account();
                this.PutAccountData(account);
                if (j == 1)
                 {
                MessageBox.Show("Wrong date entry");
                this.PutAccountData(account);
                 }
                else if ( g == 1)
                {
                MessageBox.Show("Wrong Balance input");
                this.PutAccountData(account);
                }
                customer = new Customer();
                
                try
                {
                    account.AccountID = AccountDB.AddAccount(account);
                    this.z = account.AccountID;
                this.PutCustomerData(customer);
                if (l == 1)
                {
                    MessageBox.Show("Wrong Phone Input");
                    this.PutCustomerData(customer);
                }
                customer.CustomerID = CustomerDB.AddCustomer(customer);
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
        }

        private void PutCustomerData(Customer customer)
        {
            l = 0;
            customer.Name = txtName.Text;
            customer.Address = txtAddress.Text;
            try { customer.Phone = Convert.ToInt32(txtPhone.Text); }
            catch { l = 1; }
            customer.State = cboStates.SelectedValue.ToString();
            customer.AccountID = this.z;
        }
        private int j;
        private int g;
        private void PutAccountData(Account account)
        {
            j = 0;
            g = 0;
            try { account.DateOpened = Convert.ToDateTime(txtDateOpened.Text); }
            catch { j = 1; }
            try { account.Balance = Convert.ToInt32(txtBalance.Text); }
            catch { g = 1; }
            
            if (cboAccountTypes.SelectedIndex == 0)  account.AccountTypeID = 10000;
            else if (cboAccountTypes.SelectedIndex == 1)  account.AccountTypeID = 10001;
            else if (cboAccountTypes.SelectedIndex == 2)  account.AccountTypeID = 10002;

        }

     

        private void txtName_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
