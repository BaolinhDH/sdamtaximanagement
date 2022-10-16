using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiManagementAssignment
{
    public class RankManager
    {
        // Properties
        private Dictionary<int, Rank> Ranks { get; } = new Dictionary<int, Rank>();

        public RankManager() // Constructor. Apparently adds three ranks automatically by default
        {
            // Properties of ranks to be added...?
            int[] rankId = {1, 2, 3};
            int[] rankCapacity = {5, 2, 4};

            for(int i = 0; i <= 2; i++)
            {
                this.Ranks.Add(rankId[i], new Rank(rankId[i], rankCapacity[i])); // Add some ranks with specific id and rank space I guess????? sure whatever
            }
        }

        public bool AddTaxiToRank(Taxi t, int rankId) // Add taxzi to a rank if possible
        {
            if (!this.Ranks.ContainsKey(rankId)) // Rank ID must exist
            {
                return false;
            }
            else
            {
                return this.Ranks[rankId].AddTaxi(t); // Call AddTaxi method of the rank and returns its output (bool of whether taxi successfully added to rank)
            }
        }

        public Rank FindRank(int rankId) // Search for rank by key and returns it or null if not found
        {
            if (this.Ranks.ContainsKey(rankId))
            {
                return this.Ranks[rankId];
            }
            else
            {
                return null;
            }
        }

        public Taxi FrontTaxiInRankTakesFare(int rankId, string destination, double agreedPrice) // Front taxi of parameter rank takes fare
        {
            if (!this.Ranks.ContainsKey(rankId)) // Rank ID must exist
            {
                return null;
            }
            else
            {
                return this.Ranks[rankId].FrontTaxiTakesFare(destination, agreedPrice); // Call FrontTaxiTakesFare method of rank
            }
        }
    }
}
