using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankingApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
                switch(Console.ReadLine())
                {
                    case "1":
                        break;
                    case "2":
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
    }
}
