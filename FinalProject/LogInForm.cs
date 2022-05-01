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

namespace FinalProject
{
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
        }
        public void LoginEventFunction(LogIn logn, bool status)
        {
            if (status)
            {
                MessageBox.Show(String.Format("Welcome {0}", logn.Username));
                MainForm MainForm = new MainForm();
                MainForm.Show();
            }
            else { MessageBox.Show("Please check your credentials"); }
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            LogInDB log = new LogInDB();
            log.LoginEvent += new LoginEventHandler(LoginEventFunction);
            log.Login(txtUsername.Text, txtPassword.Text);
            }
        }

  

}

