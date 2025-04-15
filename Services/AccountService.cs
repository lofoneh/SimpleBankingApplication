using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleBankingApplication.Models;

namespace SimpleBankingApplication.Services
{
    public class AccountService
    {
        private readonly String FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"AccountData.json");

        private List<Customer> customers = new List<Customer>();

        private int nextIdForCustomer = 1;
        private int nextIdForAccount = 1;

        public AccountService()
        {
            LoadDataFromJson();
        }

        public void CreateNewCustomer(string firstName, string lastName, DateTime dob, string address, string phoneNumber, string gender, string accountType)
        {
            var newCustomer = new Customer
            {
                CustomerId = nextIdForCustomer++,
                FirstName = firstName,
                LastName = lastName,
                DOB = dob,
                Address = address,
                PhoneNumber = phoneNumber,
                Gender = gender,
                AccountType = accountType
            };
            Console.WriteLine("Wait, account details are being created...");
            newCustomer.Account = GenerateAccountDatails(accountType, firstName);
            customers.Add(newCustomer);
            Console.WriteLine("Registered Successfully");
            SaveDataToJson();
        }

        public bool GetCustomer(string userName, string password)
        {
            var customer = customers.FirstOrDefault(c => c.Account.UserName == userName && c.Account.Password == password);
            if (customer != null)
            {
                Console.WriteLine($"Welcome {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Account Number: {customer.Account.AccountNumber}");
                Console.WriteLine($"Balance: {customer.Account.Balance}");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid credentials. Please try again.");
                return false;
            }
        }

        private Account GenerateAccountDatails(string accType, string firstName)
        {
            var accountDetails = new Account
            {
                AccountId = nextIdForAccount++,
                Balance = GetMinimumBalance(accType),
                UserName = GenerateUserName(firstName),
                Password = GeneratePassword(),
                AccountNumber = GenerateAccountNumber()
            };
            return accountDetails;
        }

        private string GenerateAccountNumber()
        {
            return "HDFC" + new Random().Next(100000, 999999);
        }

        private string GeneratePassword()
        {
            return "Pass" + "@" + new Random().Next(10000, 99999);
        }

        private string GenerateUserName(string name)
        {
            return name.ToLower() + "hdfc" + new Random().Next(10000, 99999);
        }

        private decimal GetMinimumBalance(string accType)
        {
            decimal minimumBalance = 0;
            if (accType == "Savings")
            {
                minimumBalance = 1000;
            }
            else if (accType == "Current")
            {
                minimumBalance = 5000;
            }
            return minimumBalance;
        }

        private void SaveDataToJson()
        {
            var json = JsonConvert.SerializeObject(customers, Formatting.Indented);
            File.WriteAllText(FilePath, json);
            Console.WriteLine("Data Saved Successfully");
        }

        private void LoadDataFromJson()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                customers = JsonConvert.DeserializeObject<List<Customer>>(json);
                Console.WriteLine("Data Loaded Successfully");
            }
        }
    }
}

