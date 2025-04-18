using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleBankingApplication.Models;

namespace SimpleBankingApplication.Services
{
    public class BankService
    {
        private readonly String FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"AccountData.json");
        private String FilePathTransactionHistory;

        private List<Customer> Accounts = new List<Customer>();
        private List<TransactionHistory> TransactionHistories = new List<TransactionHistory>();

        private int newId = 1;


        public BankService(string userName)
        {
            FilePathTransactionHistory = $"TransactionHistories/{userName}/{userName}_transactionhistory.json";
            LoadDataFromJson();
            LoadDataFromJsonTransactionHistory();
        }

        public void DepositAmount(string userName, decimal amount)
        {
            var account = Accounts.FirstOrDefault(a => a.Account.UserName == userName);
            if (account != null)
            {
                account.Account.Balance += amount;
                var bal = account.Account.Balance;

                if (!Directory.Exists($"TransactionHistories/{account.Account.UserName}"))
                {
                    Directory.CreateDirectory($"TransactionHistories/{account.Account.UserName}");
                }


                var TransactionHistoryData = new TransactionHistory
                {
                    TransactionHistoryId = newId++,
                    AccountId = account.Account.AccountId,
                    Date = DateTime.Now,
                    TransactionType = "Deposit",
                    Amount = amount,
                    Balance = bal
                };
                TransactionHistories.Add(TransactionHistoryData);
                Console.WriteLine($"Deposited {amount} to {userName}'s account. New balance: {bal}");
                SaveDataToJson();
                SaveDataToJsonTransactionHistory();
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        public void WithdrawAmount(string userName, decimal amount)
        {
            var account = Accounts.FirstOrDefault(a => a.Account.UserName == userName);
            if (account != null)
            {
                if (account.Account.Balance >= amount)
                {
                    account.Account.Balance -= amount;
                    var bal = account.Account.Balance;
                    var TransactionHistoryData = new TransactionHistory
                    {
                        TransactionHistoryId = newId++,
                        AccountId = account.Account.AccountId,
                        Date = DateTime.Now,
                        TransactionType = "Withdraw",
                        Amount = amount,
                        Balance = bal
                    };
                    TransactionHistories.Add(TransactionHistoryData);
                    Console.WriteLine($"Withdrew {amount} from {userName}'s account. New balance: {bal}");
                    SaveDataToJson();
                    SaveDataToJsonTransactionHistory();
                }
                else
                {
                    Console.WriteLine("Insufficient funds.");
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        public void CheckBalance(string userName)
        {
            var account = Accounts.Find(b => b.Account.UserName == userName);
            if (account != null)
            {
                var bal = account.Account.Balance;
                Console.WriteLine($"Available balance is: {bal}");
            }
        }

        private void SaveDataToJson()
        {
            var json = JsonConvert.SerializeObject(Accounts, Formatting.Indented);
            File.WriteAllText(FilePath, json);
            Console.WriteLine("Data Saved Successfully");
        } 
        
        private void SaveDataToJsonTransactionHistory()
        {
            if (string.IsNullOrEmpty(FilePathTransactionHistory))
            {
                throw new InvalidOperationException("FilePathTransactionHistory is not set.");
            }
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
                if (TransactionHistories != null)
                {
                    newId = TransactionHistories.Max(th => th.TransactionHistoryId) + 1;
                }
                else
                {
                    newId = 1;
                }
                Console.WriteLine("Data Loaded Successfully");
            }
        }
    }

}
