using MS.AFORO255.Deposit.Repositories;
using MS.AFORO255.Withdrawal.Models;

namespace MS.AFORO255.Withdrawal.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ContextDatabase _contextDatabase;

        public TransactionService(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }

        public Transaction Withdrawal(Transaction transaction)
        {
            _contextDatabase.Transaction.Add(transaction);
            _contextDatabase.SaveChanges();
            return transaction;
        }

        public Transaction WithdrawalReverse(Transaction transaction)
        {
            _contextDatabase.Transaction.Add(transaction);
            _contextDatabase.SaveChanges();
            return transaction;
        }
    }
}
