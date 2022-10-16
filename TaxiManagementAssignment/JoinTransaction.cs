using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiManagementAssignment
{
    public class JoinTransaction : Transaction
    {
        // Properties
        public int TaxiNum { get; }
        public int RankId { get; }

        public JoinTransaction(DateTime transactionDatetime, int taxiNum, int rankId) : base("Join", transactionDatetime) // Constructor
        {
            this.TaxiNum = taxiNum;
            this.RankId = rankId;
        }

        public override string ToString() // Return description of transaction as a string
        {
            string now = this.TransactionDatetime.ToString("dd/MM/yyyy HH:mm"); // Time of transaction, formatted

            string res = String.Format("{0} Join      - Taxi {1} in rank {2}", now, this.TaxiNum, this.RankId); // The final string

            return res;
        }
    }
}
