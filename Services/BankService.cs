using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleBankingApplication.Models;

namespace SimpleBankingApplication.Services
{
    public class BankService
    {
        private readonly String FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"AccountData.json");
        private readonly String FilePathPassBook = Path.Combine(Directory.GetCurrentDirectory(), @"PassbookData.json");

        private List<Customer> Accounts = new List<Customer>();
        private List<Passbook> Passbooks = new List<Passbook>();

        private int newId = 1;


        public BankService()
        {
            LoadDataFromJson();
            LoadDataFromJsonPassBook();
        }

        public void DepositAmount(string userName, decimal amount)
        {
            var account = Accounts.FirstOrDefault(a => a.Account.UserName == userName);
            if (account != null)
            {
                account.Account.Balance += amount;
                var bal = account.Account.Balance;

                var passbookData = new Passbook
                {
                    PassbookId = newId++,
                    AccountId = account.Account.AccountId,
                    Date = DateTime.Now,
                    TransactionType = "Deposit",
                    Amount = amount,
                    Balance = bal
                };
                Passbooks.Add(passbookData);
                Console.WriteLine($"Deposited {amount} to {userName}'s account. New balance: {bal}");
                SaveDataToJson();
                SaveDataToJsonPassBook();
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
                    var passbookData = new Passbook
                    {
                        PassbookId = newId++,
                        AccountId = account.Account.AccountId,
                        Date = DateTime.Now,
                        TransactionType = "Withdraw",
                        Amount = amount,
                        Balance = bal
                    };
                    Passbooks.Add(passbookData);
                    Console.WriteLine($"Withdrew {amount} from {userName}'s account. New balance: {bal}");
                    SaveDataToJson();
                    SaveDataToJsonPassBook();
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
                //var passbookData = new Passbook
                //{
                //    PassbookId = newId++,
                //    AccountId = account.Account.AccountId,
                //    Date = DateTime.Now,
                //    Balance = bal
                //};
                //Passbooks.Add(passbookData);
                //SaveDataToJson();
                //SaveDataToJsonPassBook();
            }
        }

        private void SaveDataToJson()
        {
            var json = JsonConvert.SerializeObject(Accounts, Formatting.Indented);
            File.WriteAllText(FilePath, json);
            Console.WriteLine("Data Saved Successfully");
        } 
        
        private void SaveDataToJsonPassBook()
        {
            var json = JsonConvert.SerializeObject(Passbooks, Formatting.Indented);
            File.WriteAllText(FilePathPassBook, json);
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
        
        private void LoadDataFromJsonPassBook()
        {
            if (File.Exists(FilePathPassBook))
            {
                var json = File.ReadAllText(FilePathPassBook);
                Passbooks = JsonConvert.DeserializeObject<List<Passbook>>(json);
                Console.WriteLine("Data Loaded Successfully");
            }
        }
    }

}
