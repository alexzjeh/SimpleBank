using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public class SavingsAccount : Account
    {

        public SavingsAccount() { }

        public double Interest 
        {      
            get { return Interest; }
            set { Interest = 0.06; }
        }

        public override void printdata()
        {
            MessageBox.Show("Account ID : " + this.AccountID + " Balance : " + this.Balance);
        }

    }
}
