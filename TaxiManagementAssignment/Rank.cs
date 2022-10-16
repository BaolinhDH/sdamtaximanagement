using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiManagementAssignment
{
    public class Rank
    {
        // Properties
        public int Id { get; }
        public int NumberOfTaxiSpaces { get; }
        public List<Taxi> TaxiSpace { get; } = new List<Taxi>();

        public Rank(int rankId, int newNumberOfTaxiSpaces) // Constructor
        {
            this.Id = rankId;
            this.NumberOfTaxiSpaces = newNumberOfTaxiSpaces;
        }

        public bool AddTaxi(Taxi t) // Add Taxi to back of Rank. Returns boolean for success of operation
        {
            if(this.TaxiSpace.Count < NumberOfTaxiSpaces && t.Rank == null && t.Destination == "") // Rank is not yet full and taxi has no destination or rank
            {
                this.TaxiSpace.Add(t);
                t.Rank = this;
                return true;
            }
            else
            {
                return false;
            }
        }

        public Taxi FrontTaxiTakesFare(string destination, double agreedPrice)
        {
            if(this.TaxiSpace.Count > 0)
            {
                Taxi t = this.TaxiSpace[0];
                this.TaxiSpace.RemoveAt(0);

                t.AddFare(destination, agreedPrice); // Access function in Taxi class to add destination and fare

                return t; // Return the Taxi
            }
            else
            {
                Taxi t = null;
                return t; // Return Null
            }
        }
    }
}
