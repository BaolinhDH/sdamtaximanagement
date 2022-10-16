using System;
using System.Collections.Generic;

namespace TaxiManagementAssignment
{
    class Program
    {
        public static void DisplayMenu() // Print menu of basic commands
        {
            Console.WriteLine("1 - Drop a taxi fare");
            Console.WriteLine("2 - Enter a taxi into a rank");
            Console.WriteLine("3 - Deploy the first taxi from a rank");
            Console.WriteLine("4 - View the financial report");
            Console.WriteLine("5 - View the location of all taxis");
            Console.WriteLine("6 - View the transaction log");
            Console.WriteLine("7 - Exit the program");
        }

        public static void DisplayResults(List<string> results) // Print list of strings
        {
            foreach(string i in results)
            {
                Console.WriteLine(i);
            }
        }

        public static double ReadDouble(string prompt) // Parse doubles from a string. Repeat until double is valid
        {
            double res = 0;
            bool valid = false; // Whether or not the entered string is valid

            while(valid != true)
            {
                try
                {
                    Console.WriteLine(prompt);
                    res = Convert.ToDouble(Console.ReadLine());
                    valid = true;
                }
                catch (FormatException)
                {
                    res = 0;
                    valid = false;
                    Console.WriteLine("That is not a valid number.");
                }
            }

            return res;
        }

        public static int ReadInteger(string prompt) // Parse integer from a string. Repeat until integer is valid
        {
            int res = 0;
            bool valid = false; // Whether or not the entered string is valid for conversion

            while (valid != true)
            {
                try
                {
                    Console.WriteLine(prompt);
                    res = Convert.ToInt16(Console.ReadLine());
                    valid = true;
                }
                catch (FormatException)
                {
                    res = 0;
                    valid = false;
                    Console.WriteLine("That is not a valid integer.");
                }
            }

            return res;
        }

        public static bool ReadBoolean(string prompt) // Parse boolean from a string. Repeat until boolean is valid
        {
            bool res = false;
            bool valid = false; // Whether or not the entered string is valid for conversion

            while (valid != true)
            {
                try
                {
                    Console.WriteLine(prompt);
                    res = Convert.ToBoolean(Console.ReadLine());
                    valid = true;
                }
                catch (FormatException)
                {
                    res = false;
                    valid = false;
                    Console.WriteLine("That is not a valid boolean.");
                }
            }

            return res;
        }

        static void Main(string[] args)
        {
            RankManager rnkMgr = new RankManager();
            TaxiManager txiMgr = new TaxiManager();
            TransactionManager trnMgr = new TransactionManager();

            UserUI userInterface = new UserUI(rnkMgr, txiMgr, trnMgr);

            // The UI and all managers are now instantiated

            Console.WriteLine("Welcome to the Taxi Management Program.");

            int userAnswer = 0;

            while(userAnswer != 7)
            {
                DisplayMenu(); // Display the user menu

                userAnswer = ReadInteger("Select an Operation using numbers 1 to 7:");
                
                if(userAnswer == 1) // TaxiDropsFare()
                {
                    int taxiId = ReadInteger("Enter the ID of the taxi dropping its fare:");
                    bool farePaid = ReadBoolean("Was the fare paid? True/False:");

                    DisplayResults(userInterface.TaxiDropsFare(taxiId, farePaid)); // Execute the method and print its return
                }
                else if(userAnswer == 2) // TaxiJoinsRank()
                {
                    int taxiId = ReadInteger("Enter the ID of the taxi joining a rank:");
                    int rankId = ReadInteger("Enter the ID of the rank:");

                    DisplayResults(userInterface.TaxiJoinsRank(taxiId, rankId)); // Execute the method and print its return
                }
                else if(userAnswer == 3) // TaxiLeavesRank()
                {
                    int taxiId = ReadInteger("Enter the ID of the rank:");
                    Console.WriteLine("Enter the taxi's destination:");
                    string destination = Console.ReadLine();
                    double agreedPrice = ReadDouble("Enter the price of the fare:");

                    DisplayResults(userInterface.TaxiLeavesRank(taxiId, destination, agreedPrice)); // Execute the method and print its return
                }
                else if (userAnswer == 4) // ViewFinancialReport()
                {
                    DisplayResults(userInterface.ViewFinancialReport());
                }
                else if (userAnswer == 5) // ViewTaxiLocations()
                {
                    DisplayResults(userInterface.ViewTaxiLocations());
                }
                else if (userAnswer == 6) // ViewTransactionlog()
                {
                    DisplayResults(userInterface.ViewTransactionLog());
                }
                else if (userAnswer == 7)
                {
                    Console.WriteLine("The Taxi Management Program will now be closed. Rank configuration, taxis, and transaction logs will not be saved.");
                }
                else
                {
                    Console.WriteLine("That is not a valid option.");
                }
            }
        }
    }

}
