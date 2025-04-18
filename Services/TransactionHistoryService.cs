using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleBankingApplication.Models;

namespace SimpleBankingApplication.Services
{
    internal class TransactionHistoryService
    {
        private readonly String FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"AccountData.json");
        private readonly String FilePathTransactionHistory;

        private List<TransactionHistory> TransactionHistories = new List<TransactionHistory>();
        private List<Customer> Accounts = new List<Customer>();

        private readonly string TransactionHistoryPath;


        public TransactionHistoryService(string userName)
        {   
            TransactionHistoryPath = $"TransactionHistories/{userName}_transactionhistory.txt";

            if (!Directory.Exists($"TransactionHistories/{userName}"))
            {
                Directory.CreateDirectory($"TransactionHistories/{userName}");
            }

            LoadDataFromJson();
            LoadDataFromJsonTransactionHistory();

            if (!File.Exists(TransactionHistoryPath))
            {
                using (StreamWriter writer = new StreamWriter(TransactionHistoryPath, true))
                {
                    writer.WriteLine("--------------------------------------------------");
                    writer.WriteLine($"      Transaction History for {userName}         ");
                    writer.WriteLine("==================================================");
                    writer.WriteLine("Date   | TransactionType   | Amount      | Balance");
                    writer.WriteLine("--------------------------------------------------");
                }
            }
        }

        public void RecordTransaction(string userName, string transactionType, decimal amount, decimal balance)
        {
            using (StreamWriter writer = new StreamWriter(TransactionHistoryPath, true))
            {
                writer.WriteLine($"{DateTime.Now} | {transactionType} | {amount} | {balance}");
            }
        }

        public void PrintTransactionHistory(string userName)
        {
            var account = Accounts.Find(a => a.Account.UserName == userName);
            if (account != null)
            {
                var data = TransactionHistories.Find(th => th.AccountId == account.Account.AccountId);
                if (data != null) {
                    using (StreamWriter writer = new StreamWriter(TransactionHistoryPath, true))
                    {
                        string date = data.Date.ToString("yyyy-MM-dd HH:MM:ss");
                        writer.WriteLine($"{date, -18} | {data.TransactionType, -16} | {data.Amount, 8:C} | {data.Balance, 9:C}");
                    }
                    Console.WriteLine("Transaction History printed successfully");
                }
                else
                {
                    Console.WriteLine("No transaction history found for Account ID: " + account.Account.AccountId);
                }

            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        private void SaveDataToJson()
        {
            var json = JsonConvert.SerializeObject(TransactionHistories, Formatting.Indented);
            File.WriteAllText(FilePath, json);
            Console.WriteLine("Data Saved Successfully");
        }

        private void SaveDataToJsonTransactionHistory()
        {
            var json = JsonConvert.SerializeObject(TransactionHistories, Formatting.Indented);
            File.WriteAllText(FilePathTransactionHistory, json);
            Console.WriteLine("Data Saved Successfully");
        }

        private void LoadDataFromJson()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                Accounts = JsonConvert.DeserializeObject<List<Customer>>(json);
                Console.WriteLine("Data Loaded Successfully");
            }
        }

        private void LoadDataFromJsonTransactionHistory()
        {
            if (File.Exists(FilePathTransactionHistory))
            {
                var json = File.ReadAllText(FilePathTransactionHistory);
                TransactionHistories = JsonConvert.DeserializeObject<List<TransactionHistory>>(json);
                Console.WriteLine("Data Loaded Successfully");
            }
        }
    }
}
