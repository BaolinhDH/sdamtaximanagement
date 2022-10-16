using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiManagementAssignment
{
    public class TaxiManager
    {
        // One Property :(
        private SortedDictionary<int, Taxi> Taxis { get; } = new SortedDictionary<int, Taxi>();

        public Taxi CreateTaxi(int taxiNum) // Create taxi, add to Taxis, return the Taxi Object
        {
            if (this.Taxis.ContainsKey(taxiNum)) // Checks if taxi number already in use
            {
                return this.Taxis[taxiNum];
            }
            else
            {
                Taxi t = new Taxi(taxiNum);

                this.Taxis.Add(taxiNum, t);

                return t;
            }
        }

        public Taxi FindTaxi(int taxiNum) // Search by key for and returns specific taxi object. Null if not find
        {
            if (this.Taxis.ContainsKey(taxiNum))
            {
                return this.Taxis[taxiNum];
            }
            else
            {
                return null;
            }
        }

        public SortedDictionary<int, Taxi> GetAllTaxis() // Return SortedDictionary Taxis
        {
            return this.Taxis;
        }
    }
}
