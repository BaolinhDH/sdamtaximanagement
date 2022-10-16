using System;
using System.Collections.Generic;
using System.Text;

namespace TaxiManagementAssignment
{
    public class TransactionManager
    {
        // One property (list of transactions)
        private List<Transaction> Transactions { get; } = new List<Transaction>();

        public List<Transaction> GetAllTransactions()
        {
            return this.Transactions;
        }

        public void RecordDrop(int taxiNum, bool pricePaid) // Add Drop Transaction to history
        {
            this.Transactions.Add(new DropTransaction(DateTime.Now, taxiNum, pricePaid));
        }

        public void RecordJoin(int taxiNum, int rankId) // Add Join Transaction to history
        {
            this.Transactions.Add(new JoinTransaction(DateTime.Now, taxiNum, rankId));
        }

        public void RecordLeave(int rankId, Taxi t) // Add Leave Transaction to history
        {
            this.Transactions.Add(new LeaveTransaction(DateTime.Now, rankId, t));
        }
    }
}
