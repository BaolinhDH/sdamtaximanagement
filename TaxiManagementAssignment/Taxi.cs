using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiManagementAssignment
{
    public class Taxi
    {
        // Constants
        public const string IN_RANK = "in rank";
        public const string ON_ROAD = "on the road";

        // Fields
        private Rank rank;

        // Properties
        public double CurrentFare { get; private set; } = 0;
        public string Destination { get; private set; } = "";
        public string Location { get; private set; } = ON_ROAD;
        public int Number { get; }
        public Rank Rank
        {
            get { return this.rank; }
            set
            {
                if(value == null)
                {
                    throw new Exception("Rank cannot be null");
                }
                else if(this.Destination.Length != 0 || this.CurrentFare != 0) // Fare dropped = destination empty, fare 0
                {
                    throw new Exception("Cannot join rank if fare has not been dropped");
                }
                else
                {
                    this.rank = value;
                    this.Location = Taxi.IN_RANK;
                }
            }
        }
        public double TotalMoneyPaid { get; private set; } = 0;

        public Taxi(int num) // Constructor
        {
            this.Number = num;
            this.rank = null; // Rank may also be set to null during constructor
        }

        public void AddFare(string newDestination, double agreedPrice) // Set destination and fare
        {
            this.Destination = newDestination;
            this.CurrentFare = agreedPrice;
            this.rank = null; // Rank may be null when taxi removed from rank via AddFare
        }

        public void DropFare(bool priceWasPaid) // Remove destination and fare
        {
            if(priceWasPaid == true) // Add the fare to TotalMoneyPaid if it was paid in full
            {
                this.TotalMoneyPaid += this.CurrentFare;
            }
            // Destination and Fare are dropped regardless
            this.Destination = "";
            this.CurrentFare = 0;
        }
    }
}
