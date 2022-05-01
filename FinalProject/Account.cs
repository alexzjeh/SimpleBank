using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Account
    {
        public Account() { }

        public int AccountID { get; set; }

        public DateTime DateOpened { get; set; }

        public int AccountTypeID { get; set; }

        public int Balance { get; set; }

        public virtual void printdata() { }
        
        
    }
}
