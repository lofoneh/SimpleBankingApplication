using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankingApplication.Models
{
    public class Customer
    {

        public int CustomerId { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public string AccountType { get; set; }

        public Account Account { get; set; }
    }
}
