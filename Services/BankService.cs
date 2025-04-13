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
        private readonly String FilePath = Path.Combine(Directory.GetCurrentDirectory(), "JsonData/AccountData.json");

        private List<Account> Accounts = new List<Account>();


        public BankService()
        {
            LoadDataFromJson();
        }
        private void SaveDataToJson()
        {
            var json = JsonConvert.SerializeObject(Accounts, Formatting.Indented);
            File.WriteAllText(FilePath, json);
            Console.WriteLine("Data Saved Successfully");
        }

        private void LoadDataFromJson()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                Accounts = JsonConvert.DeserializeObject<List<Account>>(json);
                Console.WriteLine("Data Loaded Successfully");
            }
        }
    }

}
