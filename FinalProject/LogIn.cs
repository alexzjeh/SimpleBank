using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class LogIn
    {
        
        public string Password { get; set; }
        public string Username { get; set; }

        public LogIn(string us, string pw)
        {
            Username = us;
            Password = pw;
        }
    }
}
