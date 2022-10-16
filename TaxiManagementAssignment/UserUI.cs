using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiManagementAssignment
{
    public class UserUI
    {
        // Properties
        private RankManager RankMgr { get; set; }
        private TaxiManager TaxiMgr { get; set; }
        private TransactionManager TransactionMgr { get; set; }

        public UserUI(RankManager rkMgr, TaxiManager txMgr, TransactionManager trMgr) // Constructor
        {
            this.RankMgr = rkMgr;
            this.TaxiMgr = txMgr;
            this.TransactionMgr = trMgr;
        }

        public List<string> TaxiDropsFare(int taxiNum, bool pricePaid)
        {
            if(this.TaxiMgr.FindTaxi(taxiNum) == null) // Check if taxi exists
            {
                List<string> res = new List<string>(); // Constructing return text
                res.Add(String.Format("Taxi {0} does not exist.", taxiNum));
                return res;
            }
            else if(this.TaxiMgr.FindTaxi(taxiNum).Destination != "") // Check if taxi has a fare (destination) to drop
            {
                this.TaxiMgr.FindTaxi(taxiNum).DropFare(pricePaid);
                this.TransactionMgr.RecordDrop(taxiNum, pricePaid);

                if(pricePaid)
                {
                    List<string> res = new List<string>(); // Constructing return text
                    res.Add(String.Format("Taxi {0} has dropped its fare and the price was paid.", taxiNum));
                    return res;
                }
                else
                {
                    List<string> res = new List<string>(); // Constructing return text
                    res.Add(String.Format("Taxi {0} has dropped its fare and the price was not paid.", taxiNum));
                    return res;
                }
            }
            else // No fare. Fare cannot be dropped
            {
                List<string> res = new List<string>(); // Constructing return text
                res.Add(String.Format("Taxi {0} has not dropped its fare.", taxiNum));
                return res;
            }
        }

        public List<string> TaxiJoinsRank(int taxiNum, int rankId) // Add taxi to rank based off taxi number. Record transaction
        {
            if(this.TaxiMgr.FindTaxi(taxiNum) == null) // Create taxi if it does not already exist
            {
                this.TaxiMgr.CreateTaxi(taxiNum);
            }

            if(this.RankMgr.AddTaxiToRank(this.TaxiMgr.FindTaxi(taxiNum), rankId)){ // Add taxi to rank and check if operation successful
                this.TransactionMgr.RecordJoin(taxiNum, rankId);

                List<string> res = new List<string>(); // Constructing return text
                res.Add(String.Format("Taxi {0} has joined rank {1}.", taxiNum, rankId));
                return res;
            }
            else
            {
                List<string> res = new List<string>(); // Constructing return text
                res.Add(String.Format("Taxi {0} has not joined rank {1}.", taxiNum, rankId));
                return res;
            }
        }

        public List<string> TaxiLeavesRank(int rankId, string destination, double agreedPrice) // Taxi attempts to leave rank. Record transaction
        {
            Taxi t = this.RankMgr.FrontTaxiInRankTakesFare(rankId, destination, agreedPrice); // Taxi leaves rank maybe

            if(t != null) // Check if operation successful
            {
                this.TransactionMgr.RecordLeave(rankId, t);

                List<string> res = new List<string>(); // Constructing return text
                res.Add(String.Format("Taxi {0} has left rank {1} to take a fare to {2} for £{3:0.00}.", t.Number, rankId, destination, agreedPrice));
                return res;
            }
            else
            {
                List<string> res = new List<string>(); // Constructing return text
                res.Add(String.Format("Taxi has not left rank {0}.", rankId));
                return res;
            }
        }

        public List<string> ViewFinancialReport() // Return list of print-ready strings that displays total money taken by all taxis in TaxiMgr as well as the sum of all money taken
        {
            List<string> res = new List<string>();
            res.Add("Financial report");
            res.Add("================");

            SortedDictionary<int, Taxi> taxis = this.TaxiMgr.GetAllTaxis(); // SortedDictionary taxis will contain all taxis to work with now

            if(taxis.Count <= 0) // There are no taxis
            {
                res.Add("No taxis, so no money taken");
            }
            else // There are taxis
            {
                double totalMoney = 0; // Total money of all taxis

                foreach (KeyValuePair<int, Taxi> taxi in taxis) // for each taxi (key, value stored in taxi)
                {
                    res.Add(string.Format("Taxi {0}      {1:0.00}", taxi.Key, taxi.Value.TotalMoneyPaid)); // Single line for each taxi with their total fare taken. No currency symbol needed apparently
                    totalMoney += taxi.Value.TotalMoneyPaid;
                }

                res.Add("           ======"); // Divider before sum
                res.Add(string.Format("Total:       {0:0.00}", totalMoney)); // Line containing sum
                res.Add("           ======"); // Final divider
            }

            return res;
        }

        public List<string> ViewTaxiLocations() // Return list of print-ready strings that displays location of all taxis in TaxiMgr
        {
            List<string> res = new List<string>(); // Constructing return text (Formatted for display)
            res.Add("Taxi locations");
            res.Add("==============");

            SortedDictionary<int, Taxi> taxis = this.TaxiMgr.GetAllTaxis(); // SortedDictionary taxis will contain all taxis to work with now

            if(taxis.Count <= 0) // There are no Taxis
            {
                res.Add("No taxis");
            }
            else // There are taxis
            {
                foreach(KeyValuePair<int, Taxi> taxi in taxis) // for each taxi (key, value stored in taxi)
                {
                    if(taxi.Value.Rank != null) // Taxi is in a rank
                    {
                        res.Add(string.Format("Taxi {0} is in rank {1}", taxi.Key, taxi.Value.Rank.Id));
                    }
                    else // Taxi is on the road
                    {
                        if (taxi.Value.Destination != "") // Taxi has not dropped fare
                        {
                            res.Add(string.Format("Taxi {0} is on the road to {1}", taxi.Key, taxi.Value.Destination));
                        }
                        else // Taxi has dropped fare
                        {
                            res.Add(string.Format("Taxi {0} is on the road", taxi.Key));
                        }
                    }
                }
            }

            return res;
        }

        public List<string> ViewTransactionLog() // Return list of print-ready strings that all transactions in TransactionMgr
        {
            List<string> res = new List<string>(); // Constructing return text
            res.Add("Transaction report");
            res.Add("==================");

            List<Transaction> transactions = this.TransactionMgr.GetAllTransactions(); // transactions will contain all transactions to work with now

            if(transactions.Count <= 0) // No transactions <//3
            {
                res.Add("No transactions");
            }
            else // There are transactions <3
            {
                foreach(Transaction transaction in transactions) // for each transaction. foreach loop is better for performance when list is accessed only once per iteration apparently
                {
                    if(transaction.TransactionType == "Join") // transaction is a join Transaction
                    {
                        res.Add(transaction.ToString()); // The message has already been formatted in Transaction subclasses :)
                    }
                    else if(transaction.TransactionType == "Drop fare") // transaction is a drop fare Transaction
                    {
                        res.Add(transaction.ToString());
                    }
                    else if(transaction.TransactionType == "Leave") // transaction is a leave Transaction
                    {
                        res.Add(transaction.ToString());
                    }
                }
            }

            return res;
        }
    }
}
