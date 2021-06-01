using MS.AFORO255.Deposit.Models;
using MS.AFORO255.Deposit.Repositories;

namespace MS.AFORO255.Deposit.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ContextDatabase _contextDatabase;

        public TransactionService(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }

        public Transaction Deposit(Transaction transaction)
        {
            _contextDatabase.Transaction.Add(transaction);
            _contextDatabase.SaveChanges();
            return transaction;
        }

        public Transaction DepositReverse(Transaction transaction)
        {
            _contextDatabase.Transaction.Add(transaction);
            _contextDatabase.SaveChanges();
            return transaction;
        }
    }
}
