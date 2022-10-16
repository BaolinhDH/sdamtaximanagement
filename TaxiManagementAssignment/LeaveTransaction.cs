using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiManagementAssignment
{
    public class LeaveTransaction : Transaction
    {
        // Properties
        public int TaxiNum { get; }
        public int RankId { get; }
        public string Destination { get; }
        public double AgreedPrice { get; }

        public LeaveTransaction(DateTime transactionDatetime, int rankId, Taxi t) : base("Leave", transactionDatetime) // Constructor
        {
            this.TaxiNum = t.Number;
            this.RankId = rankId;
            this.Destination = t.Destination;
            this.AgreedPrice = t.CurrentFare;
        }

        public override string ToString() // Return description of transaction as a string
        {
            string now = this.TransactionDatetime.ToString("dd/MM/yyyy HH:mm"); // Time of transaction, formatted

            string res = String.Format("{0} Leave     - Taxi {1} from rank {2} to {3} for £{4:0.00}", now, this.TaxiNum, this.RankId, this.Destination, this.AgreedPrice); // The final string
            // :0.00 format used to replicate monetary notation. :C may be preferable but pre-programmed tests works exclusively in euros

            return res;
        }
    }
}
