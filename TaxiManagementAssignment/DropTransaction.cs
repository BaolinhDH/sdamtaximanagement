using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiManagementAssignment
{
    public class DropTransaction : Transaction
    {
        // Properties
        public int TaxiNum { get; }
        public bool PriceWasPaid { get; }

        public DropTransaction(DateTime transactionDatetime, int taxiNum, bool priceWasPaid) : base("Drop fare", transactionDatetime) // Constructor
        {
            this.TaxiNum = taxiNum;
            this.PriceWasPaid = priceWasPaid;
        }

        public override string ToString() // Return transaction details as a string
        {
            string now = this.TransactionDatetime.ToString("dd/MM/yyyy HH:mm"); // Time of transaction, formatted
            string farePaid = ""; // Whether or not the fare was paid
            if (this.PriceWasPaid)
            {
                farePaid = "price was paid";
            }
            else
            {
                farePaid = "price was not paid";
            }

            string res = String.Format("{0} Drop fare - Taxi {1}, {2}", now, this.TaxiNum, farePaid); // The final string

            return res;
        }
    }
}
