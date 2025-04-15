using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankingApplication.Models
{
    public class TransactionHistory
    {
        public int TransactionHistoryId { get; set; }

        public int AccountId { get; set; }

        public DateTime Date { get; set; }

        public string TransactionType { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }
    }
}
