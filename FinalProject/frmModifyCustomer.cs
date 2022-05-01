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
    public partial class frmModifyCustomer : Form
    {
        public frmModifyCustomer()
        {
            InitializeComponent();
        }

        public Customer customer;
        int k;

        private void PutCustomerData(Customer customer)
        {
                k = 0;
                customer.Name = txtName.Text;
                customer.Address = txtAddress.Text;
                try { customer.Phone = Convert.ToInt32(txtPhone.Text); }
                catch { k = 1; }
                customer.State = cboStates.SelectedValue.ToString();
        }

      

        private void frmModifyCustomer_Load(object sender, EventArgs e)
        {
            this.LoadStateComboBox();
            this.Text = "Modify Customer";
            this.DisplayCustomer();
        }
        private void DisplayCustomer()
        {
            txtName.Text = customer.Name;
            txtAddress.Text = customer.Address;
            txtPhone.Text = Convert.ToString(customer.Phone); 
            cboStates.SelectedValue = customer.State;
        }

        private void LoadStateComboBox()
        {
            List<State> states = new List<State>();
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
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            k = 0;
            if (IsValidData())
            {
                Customer newCustomer = new Customer();
                newCustomer.CustomerID = customer.CustomerID;
                this.PutCustomerData(newCustomer);
                if (k==1)
                    {
                    MessageBox.Show("Wrong Phone Input");
                    this.DialogResult = DialogResult.Retry;
                    this.PutCustomerData(newCustomer);
                }
                try
                {
                    if (!CustomerDB.UpdateCustomer(customer, newCustomer))
                    {
                        MessageBox.Show("Another user has updated or " +
                            "deleted that customer.", "Database Error");
                        this.DialogResult = DialogResult.Retry;
                    }
                    else
                    {
                        customer = newCustomer;
                        this.DialogResult = DialogResult.OK;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
                
            
        }
        private bool IsValidData()
        {
            return
                Validator.IsPresent(txtName) &&
                Validator.IsPresent(txtAddress) &&
                Validator.IsPresent(txtPhone) &&
                Validator.IsPresent(cboStates);

        }
    }
}
