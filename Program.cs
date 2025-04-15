using System;
using SimpleBankingApplication.Services;

namespace SimpleBankingApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AccountService accountService = new AccountService();
            BankService bankService = new BankService();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("-----Welcome to Collage Banking System----------");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("****Menu****");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("1. ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Register");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("2. ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Login");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("3. ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Exit");

                Console.Write("Enter your choice: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Register(accountService);
                        break;
                    case "2":
                        string userName;
                        if (login(accountService, out userName))
                        {
                            Console.WriteLine("Login successful.");
                            Console.WriteLine($"Welcome {userName} To Collage Banking System.");
                            while (true)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("****Menu****");

                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("1. ");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Deposit");

                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("2. ");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Withdraw");

                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("3. ");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Print Passbook");

                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("4. ");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Check Balance");

                                Console.Write("Enter your choice: ");
                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        Deposit(bankService, userName);
                                        break;
                                    case "2":
                                        break;
                                    case "3":
                                        break;
                                    case "4":
                                        break;
                                    default:
                                        Console.WriteLine("Invalid choice, please try again");
                                        break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Login failed.");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Thank you for using our banking system.\nGood Bye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }


        }

        static void Register(AccountService accountService)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n-----Registering a new customer-----");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nEnter First Name: ");
            string firstName = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nEnter Second Name: ");
            string secondName = Console.ReadLine();

            Console.Write("\nEnter Date of Birth (yyyy-mm-dd): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.Write("\nEnter Address: ");
                string address = Console.ReadLine();

                Console.Write("\nEnter Phone Number: ");
                string phoneNumber = Console.ReadLine();

                Console.Write("\nEnter the Gender(Male/Female): ");
                string gender = Console.ReadLine();

                Console.Write("\nEnter the Account Type(Savings/Current): ");
                string accType = Console.ReadLine();

                if (accType != "Savings" && accType != "Current")
                {
                    Console.WriteLine("Invalid account type. Please enter either 'Savings' or 'Current'.");
                    return;
                }

                accountService.CreateNewCustomer(firstName, secondName, date, address, phoneNumber, gender, accType);
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter the date in 'yyyy-mm-dd' format.");
            }
        }

        static bool login(AccountService accountService, out string user)
        {
            user = "0";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n-----Login-----");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nEnter User Name: ");
            string userName = Console.ReadLine();
            user = userName;
            if (string.IsNullOrEmpty(userName))
            {
                Console.WriteLine("User name cannot be empty.");
                return false;
            }
            Console.Write("\nEnter Password: ");
            string password = Console.ReadLine();
            return accountService.GetCustomer(userName, password);
        }

        static void Deposit(BankService bankService, string userName)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n-----Deposit-----");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nEnter Amount to Deposit: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                bankService.DepositAmount(userName, amount);
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a valid number.");
            }
        }
    }
}
