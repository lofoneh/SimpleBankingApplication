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
    public class AccountService
    {
        private readonly String FilePath = Path.Combine(Directory.GetCurrentDirectory(), "JsonData/AccountData.json");

        private List<Customer> customers = new List<Customer>();

        private int nextIdForCustomer = 1;
        private int nextIdForAccount = 1;

        public AccountService()
        {
            LoadDataFromJson();
        }

        private void CreateNewCustomer(string name, DateTime dob, string address, string phoneNumber, string gender, string accountType)
        {
            var newCustomer = new Customer
            {
                CustomerId = nextIdForCustomer++,
                Name = name,
                DOB = dob,
                Address = address,
                PhoneNumber = phoneNumber,
                Gender = gender,
                AccountType = accountType
            };
            Console.WriteLine("Wait, account details are being created...");
        }

        private Account GenerateAccountDatails(string accType)
        {
            Accou
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

