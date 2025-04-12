using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankingApplication.Models
{
    public class Account
    {
        
        public int AccountId { get; set; }

        public decimal Balance { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string AccountNumber { get; set; }
    }
}
