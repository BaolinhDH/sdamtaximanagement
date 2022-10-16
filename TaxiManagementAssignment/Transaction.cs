using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiManagementAssignment
{
    abstract public class Transaction
    {
        // Properties
        public DateTime TransactionDatetime { get; }
        public string TransactionType { get; }

        public Transaction(string type, DateTime dt) // Constructor
        {
            this.TransactionDatetime = dt;
            this.TransactionType = type;
        }

        abstract public override string ToString(); // Return description of transaction as a string
    }
}
