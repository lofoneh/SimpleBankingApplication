﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankingApplication.Models
{
    public class Passbook
    {
        public int PassbookId { get; set; }

        public int AccountId { get; set; }

        public DateTime Date { get; set; }

        public string TransactionType { get; set; }

        public decimal Amount { get; set; }
    }
}
